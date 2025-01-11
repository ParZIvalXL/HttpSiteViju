using System.Text.RegularExpressions;
using Templator;
using Templator.TemplateClasses;

namespace Templator;

public class CustomTemplator
{
    public string ReplaceCommentWithTag(string template, string tag, string comment)
    {
        string temp = "<!--" + comment + "-->";
        return template.Replace(temp, tag);
    }
    public string RenderLogged(string template)
    {
        return BlockRemover(template, "removeOnLogin");
    }
    public string RenderFilmSite(FilmInfoTemplate filmInfoTemplate)
    {
        var template = File.ReadAllText(@"public/FilmPage.html");
        string res = Render(template, filmInfoTemplate);
        res = res.Replace("<--!player-->", filmInfoTemplate.vklink);
        return res;
    }
    
    public string CreateCategoryList(string order, List<FilmCardTemplate> films, FilmCategoryTemplate filmCategoryTemplate)
    {
        var template = @"<article class=""_mainBlock_1qh4q_41"" style=""order: {order};"">
  <div class=""_contentMomentList_1qh4q_36""><h2 class=""_momentsListTitle_1qh4q_19"">{category_name}</h2>
    <div class=""_slider_1py6y_1"" tabindex=""0""
         style=""--29665418: 25%; --5efc609c: 19; --360ad19b: 20%; --b8314380: 20%; --1e34d9b2: 25%; --3f7332de: 33.333333333333336%; --518100cc: 50%; --40f5f318: 50%; --5f4f04c1: 100%;"">
      <button tabindex=""0"" class=""_button_ffsap_1 _navigationButton_1py6y_29 _buttonPrev_1py6y_33"" title=""""
              type=""button"" aria-label=""Предыдущий слайд"" style=""display: none;""><span class=""_buttonIcon_1py6y_37""><div
        class=""_icon_hxlm4_1 _icon_ekk49_1"" style=""width: 24px; height: 24px;""><svg class=""_left_ekk49_17""
                                                                                    viewBox=""0 0 24 24"" fill=""none""
                                                                                    xmlns=""http://www.w3.org/2000/svg""><!---->
        <!----><path fill-rule=""evenodd"" clip-rule=""evenodd""
                     d=""M3.2045 16.7955C3.64384 17.2348 4.35616 17.2348 4.7955 16.7955L12 9.59099L19.2045 16.7955C19.6438 17.2348 20.3562 17.2348 20.7955 16.7955C21.2348 16.3562 21.2348 15.6438 20.7955 15.2045L12.7955 7.2045C12.3562 6.76517 11.6438 6.76517 11.2045 7.2045L3.2045 15.2045C2.76517 15.6438 2.76517 16.3562 3.2045 16.7955Z""
                     fill=""currentColor""></path></svg></div></span></button>
      <div class=""_slides_1py6y_15 _transition_1py6y_21"" style=""transform: translateX(0px);"">
        <!--slides_holder-->
      </div>
      <button tabindex=""0"" class=""_button_ffsap_1 _navigationButton_1py6y_29 _buttonNext_1py6y_34"" title=""""
              type=""button"" aria-label=""Предыдущий слайд""><span class=""_buttonIcon_1py6y_37""><div
        class=""_icon_hxlm4_1 _icon_ekk49_1"" style=""width: 24px; height: 24px;""><svg class=""_right_ekk49_13""
                                                                                    viewBox=""0 0 24 24"" fill=""none""
                                                                                    xmlns=""http://www.w3.org/2000/svg"">
        <path fill-rule=""evenodd"" clip-rule=""evenodd""
                     d=""M3.2045 16.7955C3.64384 17.2348 4.35616 17.2348 4.7955 16.7955L12 9.59099L19.2045 16.7955C19.6438 17.2348 20.3562 17.2348 20.7955 16.7955C21.2348 16.3562 21.2348 15.6438 20.7955 15.2045L12.7955 7.2045C12.3562 6.76517 11.6438 6.76517 11.2045 7.2045L3.2045 15.2045C2.76517 15.6438 2.76517 16.3562 3.2045 16.7955Z""
                     fill=""currentColor""></path><!----></svg></div></span></button>
    </div>
  </div>
</article>
";
        
        Console.WriteLine("Creating category " + order);
        template = template.Replace("{order}", order).Replace("{category_name}", filmCategoryTemplate.name);
        string cardsPut = "";
        foreach (var film in films)
        {
            cardsPut += CreateMovieCard(film);
        }
        template = template.Replace("<!--slides_holder-->", cardsPut);
        return template;
    }
    // TODO: Обрабатывать шаблон вида {if(gender)}<h1></h1>
    public string CreateMovieCard(FilmCardTemplate filmCardTemplate)
    {
        var template = File.ReadAllText(@"public/templates/MovieCard.html");
        string res = Render(template, filmCardTemplate);
        return res;
    }

    public string GetHtmlByTemplate(string template, string name)
    {
        return template.Replace("{name}", name);
    }
    
    public string GetHtmlByTemplate(string template, User user)
    {
        var res = template.Replace("{name}", user.Name);
        res = res.Replace("{email}", user.Email);
        return res;
    }
    
    public string BlockRemover(string template, string blockName)
    {
        string pattern = @$"<!--{blockName}-->(.*?)<!--/{blockName}-->";
        
        // Заменяем найденный текст на пустую строку
        string result = Regex.Replace(template, pattern, "", RegexOptions.Singleline);
        
        return result;
    }
    
    public string Render(string template, object data)
    {
        template = ProcessLoops(template, data);
        
        var properties = data.GetType().GetProperties();
        var result = template;
        Console.WriteLine("found properties " + properties.Length);

        foreach (var property in properties)
        {
            var placeholder = $"{{{property.Name}}}";
            var value = property.GetValue(data);
            
            if (value is DateTime dateValue)
            {
                value = dateValue.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));
            }

            result = result.Replace(placeholder, value?.ToString() ?? string.Empty);
        }

        return result;
    }
    
    private string ProcessLoops(string template, object data)
    {
        var regex = new Regex(@"\{#foreach (.*?)\}(.*?)\{\/foreach\}", RegexOptions.Singleline);
        return regex.Replace(template, match =>
        {
            var propertyName = match.Groups[1].Value;
            var loopContent = match.Groups[2].Value;

            var property = data.GetType().GetProperty(propertyName);
            if (property != null)
            {
                var collection = property.GetValue(data) as IEnumerable<object>;
                if (collection != null && collection.Any())
                {
                    var loopResult = string.Empty;
                    foreach (var item in collection)
                    {
                        loopResult += new CustomTemplator().Render(loopContent, item);
                    }
                    return loopResult;
                }
            }
            return string.Empty;
        });
    }
    
    
}

public class User
{
    public string Name { get; set; }

    public string Email { get; set; }

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }
}