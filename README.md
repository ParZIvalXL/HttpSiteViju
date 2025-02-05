
# HttpServer на примере viju.ru

Данное приложение представляет собой сайт онлайн-кинотеатр с версткой основанной на существующем онлайн кинотеатре viju.ru

Текстовое задание сайта находится в файле тз.docs
# Инициализация и запуск сервера

Склонируйте репозиторий локально

```bash
  git clone https://github.com/ParZIvalXL/HttpSiteViju
```

Откройте .sln файл проекта

Для запуска сервера запустите приложение или откройте .exe файл билда находящийся в /MyHTTPServer/bin/Debug/net7.0/MyHTTPServer.exe

После по ссылке согласно указанному домену и порту можно попасть на сайт. По умолчанию это http://localhost:7574/
## Инициализация бд
У сайта присутсвтует 5 моделей в базе данных: Пользователи, Категории фильмов, Фильмы, Карточки фильмов, Администраторы.

В пользователях находятся все пользователи с информацией о имени, телефоне, почте, пароле, и индификаторе.

Категории фильмов содержат: Название, строка с id карточек через запятую, номер для сортировки.

Карточки фильмов содержат: Название, описание (теги), айди карточки, айди фильма на который карточка отсылает при нажатии, ссылка на изображение.

Киномы карточек содержат: Название, ссылка на изображение, айди фильма

Фильмы содержат: Название фильма для отображения. Полное название фильма, Ссылка на постер фильма, ссылка на постер названия фильма, год релиза, страна(-ы) съемки, длина фильма, возрастные ограничения, жанры, звезды которые участвовали в фильме, список актеров

### Создание бд и таблиц:
```sql
CREATE DATABASE Film;
CREATE TABLE FilmCardInfo(name nvarchar(255), description nvarchar(255), img nvarchar(255), FilmI int, id int identity(1,1));
CREATE TABLE FilmCategory(name nvarchar(255), ids varchar(255), Ordering int identity(1,1));
CREATE TABLE Kinom(name nvarchar(255), url varchar(255), filmId int);
CREATE TABLE FilmCardInfo(FilmName NVARCHAR(255), FullFilmName NVARCHAR(255), FilmPoster
 NVARCHAR(255), FilmNamePoster NVARCHAR(255), FilmYear NVARCHAR(255), FilmCountries NVARCHAR(255), FilmDuration
 NVARCHAR(255), FilmAgeLimit NVARCHAR(255), FilmGenres NVARCHAR(255), FilmTagline NVARCHAR(255), FilmStarring NVARCHAR(255), FilmDescription
 NVARCHAR(255), FilmDirector NVARCHAR(255), FilmActors NVARCHAR(255), FilmWriters NVARCHAR(255), FilmProducers
 NVARCHAR(255), FilmAudioChannels NVARCHAR(255), FilmQuality NVARCHAR(255), FilmPlotSumary NVARCHAR(255), VKLink NVARCHAR(255), id INT IDENTITY(1,1));
```
 После можно приступить к заполнению бд

Для заполнения бд могут быть исопльзованы следующие команды
Заполнить бд информации фильмов:
```SQL
INSERT INTO FilmInfo (filmName, fullFilmName, filmPoster, filmNamePoster, filmYear, filmCountries, filmDuration, filmAgeLimit, filmGenres, filmTagline, filmStarring, filmDescription, filmDirector, filmActors, filmWriters, filmProducers, filmAudioChannels, filmQuality, filmPlotSumary, vklink)
VALUES (
          N'Пришельцы: Назад в будущее',
          N'Пришельцы: Назад в будущее (полное название)',
          N'URL_постера',
          N'Пришельцы: Назад в будущее (постер)',
          N'2023',
          N'Россия',
          N'120 мин',
          N'16+',
          N'Фантастика, Комедия',
          N'Слоган фильма',
          N'В главных ролях: Актер1, Актер2',
          N'Описание фильма',
          N'Режиссер',
          N'Актер1, Актер2',
          N'Сценарист1, Сценарист2',
          N'Продюсер1, Продюсер2',
          N'5.1',
          N'1080p',
          N'Краткое описание сюжета',
          N'Ссылка на VK'),(
   N'Джон Уик 3',
   N'Джон Уик 3: Парабеллум',
   N'URL_постера',
   N'Джон Уик 3 (постер)',
   N'2019',
   N'США',
   N'131 мин',
   N'18+',
   N'Боевик, Триллер',
   N'Слоган фильма',
   N'В главных ролях: Киану Ривз, Холли Берри',
   N'Описание фильма',
   N'Чад Стахелски',
   N'Киану Ривз, Холли Берри',
   N'Дерек Колстад, Шей Хаттен',
   N'Продюсер1, Продюсер2',
   N'5.1',
   N'4K',
   N'Краткое описание сюжета',
   N'Ссылка на VK'),
    (
   N'Жанна Дюбари',
   N'Жанна Дюбари (полное название)',
   N'URL_постера',
   N'Жанна Дюбари (постер)',
   N'2022',
   N'Франция',
   N'110 мин',
   N'16+',
   N'Драма, Исторический',
   N'Слоган фильма',
   N'В главных ролях: Актер1, Актер2',
   N'Описание фильма',
   N'Режиссер',
   N'Актер1, Актер2',
   N'Сценарист1, Сценарист2',
   N'Продюсер1, Продюсер2',
   N'5.1',
   N'1080p',
   N'Краткое описание сюжета',
    null),
    (
        N'Веном',
        N'Веном (полное название)',
        N'URL_постера',
        N'Веном (постер)',
        N'2018',
        N'США',
        N'112 мин',
        N'16+',
        N'Фантастика, Боевик',
        N'Слоган фильма',
        N'В главных ролях: Том Харди, Мишель Уильямс',
        N'Описание фильма',
        N'Рубен Фляйшер',
        N'Том Харди, Мишель Уильямс',
        N'Скотт Розенберг, Джефф Пинкнер',
        N'Продюсер1, Продюсер2',
        N'5.1',
        N'1080p',
        N'Краткое описание сюжета',
        N'Ссылка на VK'
    );
```
Для заполнения категорий исользуются команды
```SQL
INSERT INTO FilmCategory(name, ids)
VALUES (N'Что посмотреть?', '1,2,3,4'),
(N'Боевики', '2,1,6,4,5'),
(N'Популярное', '2,1,4,3');
```
Для заполнения карточек исользуются команды
```SQL
INSERT INTO FilmCardInfo(name, description, img, FilmId)
VALUES (N'Пришельцы: назад в будущее', N'Приключения, фантастика, боевик', 'https://media.discordapp.net/attachments/1150763616266625029/1322518598333235222/6RVjnq1qFew.jpg?ex=67824e6d&is=6780fced&hm=9f9d48176fff46b8482a1b5ff43573d81e7ebcb5f1a04ac5bf23fd83edbedce5&=&format=webp&width=1207&height=905', 1),
(N'Джон Уик 3', N'2019, Экшен, Боевик', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contents/d1257600-c049-4cab-bcea-0fa27f96b3bf/backgrounds/953mj2tdhz4yj9y23d1uzakn2giv?w=1024', 2),
(N'Жанна Дюбари', N'2023, Драма, Биография', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contentmoments/a396a3ca-4800-4daf-b119-d0a28943f4fa/previews/3n8y80tc6hxw7v1p1kq7ddn35msv?w=1200', 3),
(N'Блиндаж', N'2024, Россия, Военный', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contents/523ed011-a980-46d3-bcaf-76337751bec6/backgrounds/71iu1l34kks6901ytyf5myli09iu?w=1024',  null),
(N'Билет в один конец', N'2022, Драма, Триллер', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contents/e80a58ae-7c70-49c1-9973-b447780d4d44/backgrounds/9iapydk3ib2sk9fuobnczf3h1pny?w=1024', null),
(N'Джон Уик 2', N'2017 США Триллер', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contents/f7d387f7-7e1c-45c1-b859-e57a62c4e2df/backgrounds/a1xo0vpd0y00apxmeqybaev9h06w?w=1024', null),
(N'Веном', N'2018, США, Триллер', 'https://images-s.kinorium.com/movie/poster/453484/h280_50263893.jpg', 4);
```
Для заполнения киномов используются команды (на примере Джон Уик 3):
```SQL
INSERT INTO Kinom(name, url, filmId)
VALUES (N'Дружи с книгами', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contentmoments/d724f489-9c23-474a-a0c6-a782c40eaed7/previews/olrn3ioli7d9jgljbz450uezhjx2?w=1200', 2),
(N'Коллекция ножей Джона', 'https://rnnxdki1nv.a.trbcdn.net/Ф/production/contentmoments/ed3ac838-7b3b-460f-8465-81266647a5f9/previews/pwn66vpq6zjru0nex9uge21gn3xs?w=1200', 2),
(N'В чем сила?', 'https://rnnxdki1nv.a.trbcdn.net/viasat/production/contentmoments/122dda80-f50c-4425-bf5b-91d3dc37fbb5/previews/dcnwwq0ol7mv11utv5c210s35otk?w=1200', 2);
```