using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {
    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            var context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<BookShelfDbContext>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.Authors.Any()) {
                context.Authors.AddRange(
                    new Author {
                        AuthorId = 1,
                        BirthYear = 1947,
                        Name = "Стивен Кинг"
                    },
                    new Author {
                        AuthorId = 2,
                        BirthYear = 1821,
                        DeathYear = 1881,
                        Name = "Фёдор Достоевский"
                    },
                    new Author {
                        AuthorId = 3,
                        BirthYear = 1892,
                        DeathYear = 1973,
                        Name = "Джон Толкин"
                    },
                    new Author {
                        AuthorId = 4,
                        BirthYear = 1948,
                        Name = "Джордж Мартин"
                    }
                );
            }

            if (!context.Books.Any()) {
                context.Books.AddRange(
                   new Book {
                        BookId = 1,
                        AuthorId = 1,
                        Title = "ОНО",
                        Category = "проза",
                        Genre = "ужасы",
                        Description = "В маленьком провинциальном городке Дерри много лет назад семерым подросткам пришлось столкнуться с кромешным ужасом — живым воплощением ада. Прошли годы... Подростки повзрослели, и ничто, казалось, не предвещало новой беды. Но кошмар прошлого вернулся, неведомая сила повлекла семерых друзей назад, в новую битву со Злом. Ибо в Дерри опять льется кровь и бесследно исчезают люди. Ибо вернулось порождение ночного кошмара, настолько невероятное, что даже не имеет имени...",
                        PageAmount = 1248,
                        Price = 700.00M,
                        Publishing = "АСТ",
                        PublishingYear = 2021,
                    },
                    new Book {
                        BookId = 2,
                        AuthorId = 2,
                        Title = "Идиот",
                        Category = "проза",
                        Genre = "роман",
                        Description = "Завораживающая история трагических страстей, связавших купца Парфена Рогожина, бывшую содержанку богатого дворянина Настасью Филипповну и \"идеального человека\" князя Мышкина — беспомощного идиота в мире корысти и зла, гласящая о том, что сострадание, возможно, единственный закон человеческого бытия. Она по-прежнему актуальна и воспринимается ярко и непосредственно, будто была написана вчера.",
                        PageAmount = 640,
                        Price = 430.00M,
                        Publishing = "Эксмо",
                        PublishingYear = 2016
                    },
                    new Book {
                        BookId = 3,
                        AuthorId = 3,
                        Title = "Властелин колец",
                        Category = "проза",
                        Genre = "фэнтези",
                        Description = "Трилогия \"Властелин Колец\" бесспорно возглавляет список \"культовых\" книг ХХ века. Ее автор, Дж. Р.Р. Толкин, профессор Оксфордского университета, специалист по древнему и средневековому английскому языку, создал удивительный мир — Средиземье, который вот уже без малого пятьдесят лет неодолимо влечет к себе миллионы читателей. Великолепная кинотрилогия, снятая Питером Джексоном, в десятки раз увеличила ряды поклонников как Толкина, так и самого жанра героического фэнтези.",
                        PageAmount = 752,
                        Price = 1733.00M,
                        Publishing = "АСТ",
                        PublishingYear = 2015
                    },
                    new Book {
                        BookId = 4,
                        AuthorId = 4,
                        Title = "Игра престолов",
                        Category = "проза",
                        Genre = "фэнтези",
                        Description = "Перед вами — величественное шестикнижие \"Песнь льда и огня\". Эпическая, чеканная сага о мире Семи Королевств. О мире суровых земель вечного холода и радостных земель вечного лета. Мире лордов и героев, воинов и магов, чернокнижников и убийц — всех, кого свела воедино Судьба во исполнение древнего пророчества. О мире опасных приключений, великих деяний и тончайших политических интриг.",
                        PageAmount = 768,
                        Price = 711.20M,
                        Publishing = "АСТ",
                        PublishingYear = 2021
                    }
                );
                context.SaveChanges();
            }
        }
    }
}