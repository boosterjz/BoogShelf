using Microsoft.EntityFrameworkCore;
namespace BookShelf.Models;

public static class SeedData {

    public static void EnsurePopulated(IApplicationBuilder app) {
        StoreDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

        // if (context.Database.GetPendingMigrations().Any())
        // {
        //     context.Database.Migrate();
        // }

        context.Database.EnsureCreated();

        if (!context.Products.Any()) {
            context.Products.AddRange(
                new Product {
                    Title = "Преступление и наказание",
                    Author = "Федор Михайлович Достоевский",
                    Category = "Проза", Genre = "Роман",
                    Description = "Социально-психологический и социально-философский роман.",
                    Price = 1000M
                },
                new Product {
                    Title = "Властелин колец",
                    Author = "Джон Рональд Руэл Толкин",
                    Category = "Проза", Genre = "Фэнтези",
                    Description =
                        "Роман-эпопея английского писателя Дж. Р. Р. Толкина, одно из самых известных произведений жанра фэнтези",
                    Price = 1200M
                },
                new Product {
                    Title = "Гордость и предубеждение",
                    Author = "Джейн Остин",
                    Category = "Проза", Genre = "Роман",
                    Description = "Роман Джейн Остин, опубликованный в 1813 году.",
                    Price = 400M
                },
                new Product {
                    Title = "Автостопом по Галактике",
                    Author = "Дуглас Адамс",
                    Category = "Проза", Genre = "Фантастика",
                    Description =
                        "Юмористический фантастический роман английского писателя Дугласа Адамса. Первая книга одноимённой серии.",
                    Price = 550M
                },
                new Product {
                    Title = "Гарри Поттер и Кубок огня",
                    Author = "Джоан Роулинг",
                    Category = "Проза", Genre = "Роман",
                    Description =
                        "Четвёртая книга о приключениях Гарри Поттера, написанная английской писательницей Джоан Роулинг. В Англии опубликована в 2000 году. По сюжету Гарри Поттер против своей воли вовлекается в участие в Турнире Трёх Волшебников, и ему предстоит не только сразиться с более опытными участниками, но и разгадать загадку того, как он вообще попал на турнир вопреки правилам.",
                    Price = 500M
                },
                new Product {
                    Title = "Убить пересмешника",
                    Author = "Харпер Ли",
                    Category = "Проза", Genre = "Роман",
                    Description =
                        "Роман-бестселлер американской писательницы Харпер Ли, опубликованный в 1960 году, за который в 1961 году она получила Пулитцеровскую премию. Её успех стал вехой в борьбе за права чернокожих.",
                    Price = 600M
                },
                new Product {
                    Title = "1984",
                    Author = "Джордж Оруэлл",
                    Category = "Проза", Genre = "Роман",
                    Description =
                        "Роман-антиутопия Джорджа Оруэлла, изданный в 1949 году. Как отмечает членкор РАН М. Ф. Черныш, это самое главное и последнее произведение писателя.",
                    Price = 700M
                }
            );
            context.SaveChanges();
        }
    }
}