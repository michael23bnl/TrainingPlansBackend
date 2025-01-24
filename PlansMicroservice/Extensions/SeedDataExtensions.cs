
using TrainingPlans.Entities;

namespace TrainingPlans.Extensions;

public static class SeedDataExtensions
{
    public static void SeedPlansData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlansDbContext>();

        if (!context.Plans.Any())
        {
            context.AddRange(
                new PlanEntity {
		            Id = Guid.NewGuid(),
                    Exercises = {
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плечевого сустава" },
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу 25-20-15-10 с увеличением веса" },
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя + Разводка гантель стоя 8-10-12-15 с уменьшением веса" },
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу обратным хватом 25-20-15-12-10-8-8" },
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя 30-25-20-15-10" },
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с кривой штангой лёжа 15-12-10-8-15" },
                    new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП (пресс, вис, планка)" },
                    },

                IsPrepared = true, CreatedBy = null },
                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа на горизонтальной скамье 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа на наклонной скамье-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пуловер с гантелью лёжа поперёк скамьи -//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя одновременно + Бицепс с пустым грифом 15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя поочередно 15 по 5 раз; 10+5; 15+5; 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 10+10 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 8-8-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой широким хватом 8-8-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельным хватом 8-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },
                
                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 8-10-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев с резиной 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа узким хватом 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок одной рукой 3х15 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отведение руки в тренажере на заднюю дельту 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя 15-12-10-8-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой средним хватом-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельный хват 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя + Трицепс от лежака 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Вис 1’, пресс на коврике 3х25 1’ отдыха, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                		Id = Guid.NewGuid(),
                                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 25(гриф)-15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев 5х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний 8-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой 3х3 по 7" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Сгибание Зотмана 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга сидя 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу -//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед с блином 10 кг 5х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно 20-18-15 + Трицепс с гантелью стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс + Гиперэкстензия блин 5 кг 25+25 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда ладони наружу + Жим гантель стоя одновременно 15; 12; 10; 8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой + Трицепс с гантелью стоя 15-12-10-8 + 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на наклонной скамье с поворотами 5х18; планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу 25-20-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель сидя в упоре + Разводка гантель сидя 10+10;12+12;15+15;20+20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу с увеличением веса 20-18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Сгибание Зотмана + Трицепс с гантелями лёжа 15+25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа широкий хват наклонная скамья 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя вес максимум 5х10 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельный хват 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями вес максимум 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха 15-12-10-8 + Трицепс с гантелями лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 3х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа + Отжимание от лежака широким хватом 10+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди с увеличением веса средним хватом сверху 18-15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шраги с гантелями 8 кг 4х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя попеременно с высоким весом + Трицепс-блок 10+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Вис 1’, пресс на наклонной скамье с поворотами 4х18, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу 15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя (лучше брать не стандартный гриф из угла) 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя одной рукой в наклоне и упоре (либо за шведскую стенку, либо за гриф Смитта, можно брать вес гантель выше среднего) 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно + Молот стоя раздельно 25-20-15 + 15-12-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс 2х25, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина параллельный хват 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху широкий 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 20-18-15-12-10 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от лежака + Трицепс-блок в тренажере 25;20;15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями стоя в наклоне одновременно 2х25 (3-4 кг не больше)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2(3) кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя за голову 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 10+15 1’ отдыха (понижение веса гантель на 1 кг) 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 15-12-10-8-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель в наклоне (не бери большой вес гантель, больше прожимай лопатки) 15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка с увеличением веса 20-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 20(разминка)-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка лёжа (гири) 12(14) кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом 13,5 кг 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя попеременно 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Фр.жим стоя с гантелью 25-20-15-12-10-8 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от лежака 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти, ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя с увеличением веса 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу обратным хватом 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель в наклоне 6-5-5 кг х 15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя в наклоне блины по 2,5 кг 4х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 4х15; планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа + Отжимание от лежака широким хватом 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим Свенда 2 блина по 2,5 кг 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом с расширителями + Трицепс с гантелью стоя 15-12-10-8 + 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя (стартуем с 4-5 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой (широкий, средний, узкий хваты)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15 + 15-12-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя полная амплитуда ладони наружу 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 20-18-15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола с блином (2-5, 5 кг) 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 4х15, Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 15-12-10-8-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой хват сверху + Молот стоя поочередно 15+10 4 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа + Трицепс-блок 15+25 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 25-20-15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя + Разводка гантель стоя полная амплитуда (чередуем по 3 раза) 15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 8-10-12-15-20 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель лёжа на наклонной скамье лицом к полу 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на наклонной скамье с поворотами с набивным мячом 3х20, планка боковая 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 15(разминка)-4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя поочередно" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+15+10+25 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 5х15; планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 15+10 3 серии (спускаемся вниз на 1 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 8-8-10-10-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой средним хватом 8-8-10-10-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельным хватом 8-8-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 8-10-12-15-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 8-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев с резиной 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10;15;20;25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу 20-15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя + Разводка гантель стоя полная амплитуда (чередуем по 3 раза) 15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 25-20-15-10 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель стоя одновременно 15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на наклонной скамье с поворотами с набивным мячом 3х20, планка боковая 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 15(разминка)-4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно нижняя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+(15+15) 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 5х15; планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 3 кг 10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "(широким, средним, узким хватом) 15+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя поочередно 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Подтягивание на перекладине средним хватом с резиной 5х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельный хват 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя 25-20-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Вис 1’, пресс на коврике 3х30, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 500" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "(делаем одним весом) 15;12;10;8 с увеличением веса гантель" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев с резиной 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим Свенда 3х2,5 или 1,25 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха на скамье Скотта 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя поочередно 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "(чередуем руки по 5 раз)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Вис 1’, пресс на брусьях 4х15, Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 15+10 3 серии (спускаемся вниз на 1 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 8-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой широким, средним, узким хватом х 10(12) раз" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельным хватом 8-8-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 8-10-12-15-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 8-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев с резиной 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10;15;20;25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка с увеличением веса 20 15 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа + Разводка гантель лёжа 8;10;12;15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя с большим весом (подбираем индивидуально) 4х15 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом 15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой с увеличением веса 15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой + Трицепс-блок 20;15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Вис 1´; пресс на перекладине косые мышцы 3х15 в каждую сторону; планка 1´" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа (средний вес) 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя (средний вес) 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа шаг + 5 кг по 15 раз" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 40 кг 2х30" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха стоя 15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 25-20-15-10 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике (велосипед) 3х15 на каждую сторону" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка боковая 40’’ на каждую сторону" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 3(4) кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина с резиной хват параллельный 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола с блином 5 кг 4х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания с гирей 16 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 10-15-20-25 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок -//-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 200" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху широкий 5х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель стоя в наклоне 20-15-10 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра шаг 10 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "25-20-15 + 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом на скамье Скотта 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 4х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на брусьях 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка на одной ноге 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя -//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа шаг + 5 кг старт с 10 кг 18-15-12-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 40-50-60 кг 25-20-2х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха стоя 15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 20-15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике (велосипед) 3х15 на каждую сторону" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка боковая 40’’ на каждую сторону" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 4(5) кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола с блином 8 кг 5х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания с гирей 20 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 10-15-20-25 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок -//-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 200" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа 10+10 2 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8 2 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват обратный 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Турецкие подтягивания 5х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 4х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на брусьях 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка на одной ноге 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа с увеличением веса 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание с блином 8-10 кг 5х8(10)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа хват классический старт с 10 кг шаг 5-2,5-2,5 кг 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом хват сверху 5х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя 8-10-12-15-25 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, вис, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 10+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "25-20-15 + 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Сведение рук стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15; 12+15; 10+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа на наклонной скамье 25-20-15 средним весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя поочередно 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отведение руки лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "25-20-15-10 + 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка-кривуха широким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя одновременно" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15; 12+12;10+10;8+8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельный хват 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 8-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс-велосипед 5х20 в каждую сторону" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1,5’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от лежака широким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+15+10 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу с увеличением веса 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом 8-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шраги с гантелями 10-12-15 кг 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя полная амплитуда" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка-кривуха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15 + 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отведение руки в блоке 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя на наклонной скамье" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа полуамплитуда 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой 5+5+5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой 5+5+5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 8+12 4 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель сидя в упоре 15-12-10-8-8-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом 5х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа классический хват 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом 15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "6+6;8+8;10+10;15+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Сведение рук стоя в тренажере 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя на наклонной скамье" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с грифом в наклоне 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга сидя 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+12+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола на дельты 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина с резиной широким хватом 4х8 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа II хват 18-15-12-10-8-8 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель сидя в упоре" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с прямым грифом средним хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель сидя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отведение руки лёжа с упором на скамью 3х15 3(4) кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 18-15-12-10-8 + 5 кг, старт с 10 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Турецкие подтягивания 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка с увеличением веса 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 6-8-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье полуамплитуда 10-12-15-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха хват сверху" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15-12-10-8 + 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти с грифом хват сверху 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 1,5 кг 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка (можно брать как кривуху так и прямую, ту же с которой выполняете жим)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа хват классический (старт с 15 кг, шаг +5 кг) 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель с упором лбом на лежак 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Французский жим лёжа с кривой штангой 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями стоя в наклоне 3-4 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка 2х15 средним весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 8-10-12-15-18 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье полуамплитуда 8-10-12-15-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от лежака широким хватом 5х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой 5-5-5х3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно с блином 15 кг по 15 раз" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга сидя 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "6;8;10;12;15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа с отрицательным наклоном 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа с отрицательным наклоном 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой 5-5-5 х 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 4х15; 20; 25 30’’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя за голову 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 25-20-15-10 широким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 10-15-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8;10;12;15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 3х15; 20;25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "5-5-5 + 15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Подтягивание на перекладине с резиной широким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15-20-25 + 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа с упором на лежак (задняя дельта) 25-20-15-10 (делаем медленно)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа хват классический" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15 + 15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя на наклонной скамье" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "25;20;15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель с отрицательным наклоном 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель с положительным наклоном 3х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа полуамплитуда 15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель в наклоне" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отведение руки назад лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя с рабочим весом 4х15 40’’ отдыха - уменьшаем вес гантель 20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя за голову 18-15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу обратным хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "18;15;12;10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя на наклонной скамье 25-20-15 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+12+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа хват классический 18-15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне 8-8-10-12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка боковая 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "6-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8-8-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях с резиной" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "по 15 раз 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя дельта в Смите 25;20;15;10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "12;10;8;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина узким обратным хватом 5х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15;12;10;8;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно 25;20;15;10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 10+15 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 8-8-10-12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом 4х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу 12-10-8-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8-8-10-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа на наклонной скамье 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание с блином 8(10) кг 5х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой хват сверху" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя поочерёдно по 3 раза" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от лежака" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+15+12+25 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга сидя 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бабочка 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях с резиной 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя поочерёдно по 5 раз на руку" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа узким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 3 кг 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа с упором на лежак" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя дельта в Смите" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+10+15 3(4) круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой средним хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+12 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги в наклоне обратным хватом 15-4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель в наклоне 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа 15-12-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа (в таком же порядке весов что и на разводке обратно) 8-8-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок одной рукой обратным хватом 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями стоя в наклоне 2х25 (3-4 кг не больше)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отведение руки в кроссовере 15-12-10-8 1’ или 40’’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "4 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина параллельный хват (подтягивайте грудь к перекладине) 5х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя поочерёдно по 5 раз" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа 8-8-6-6-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 х 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от лежака 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель в упоре на лежак 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 8-8-10-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина хват сверху средний с резиной 5х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний 8-8-10-10-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой хват сверху" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя в наклоне одной рукой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10 + 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти с грифом сидя 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа в Смите с положительным наклоном 18-15-12-10-8 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 4х15 30’’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом с резиной 4х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха 5-5-5 х 3 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелью стоя в наклоне без отдыха 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимания узким хватом с упором на лежак 4х15 30(40)’’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа в Смите 18-15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье 12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Сведение рук в кроссовере 15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой 5-5-5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Французский жим лёжа с кривухой 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга сидя 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье полуамплитуда 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом-//-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой 5-5-5 х 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя поочерёдно 10+5 х 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя попеременно 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга в Смите на заднюю дельту (будет занят, тогда прямую штангу) 18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель с упором на лежак 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 5х18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 4х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот сидя поочерёдно по 5 раз 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине (косые мышцы) 4х15 на каждую сторону" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка на прямых руках 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга в Смите за спиной на заднюю дельту 18-15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 15-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом хват сверху" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "12+15 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти с грифом сидя 3хмаксимум" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8 х 3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель сидя полная амплитуда 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривухой 4-4-4 х 3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелью стоя в наклоне 3х12 без отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти с грифом сидя 3 х макс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 12+12 х 3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом с резиной 5х6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 5х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шраги с тяжелыми гантелями 4х14(18)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка на своё усмотрение" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8;10;12;15 начиная с максимального веса гантель" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель с упором на наклонную скамью на заднюю дельту 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя дельта в Смите 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди широким хватом 18-15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха 4-4-4" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим лёжа в Смите" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пуловер лёжа поперёк скамьи с гантелью" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "12+12х3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа с уменьшением веса 8-10-12-15-18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя полная амплитуда сидя 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот сидя с упором спиной поочередно по 5 раз 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг:10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8;10+10;12+12;15+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель с упором на лежак (задняя дельта) 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди с увеличением веса 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель с упором лба на лежак 15-12-10-10(8)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15х3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя 12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина хват сверху средний 12-12-10 (если не получится, то можно делать по 8 раз)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний 12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью одной рукой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8 х 3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пуловер поперек скамьи 8-10-12 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев (можно с резиной) 3х12(15)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 8-8-10-12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель сидя 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч 10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя 8+12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина 8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 2х8; 2х10;12;15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельный хват с уменьшением веса 8-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок 18(15)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти с грифом сидя хват сверху 3хmax" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа 2-5-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пуловер поперек скамьи 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель с упором на лежак на заднюю дельту 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лежа 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха 4-4-4" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот с блином 15 кг 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "12;10; 2х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга на заднюю дельту в Смите 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя поочерёдно 8+10+12 х 2 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя в упоре на конструкцию тренажёра Смит" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "20; 18; 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга хват сверху 3 х max" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги стоя за голову 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 8/10/12/15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 2х8/10/12/15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим + Разводка гантель лёжа 2х6+6; 2х8+8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пуловер поперек скамьи 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимания от брусьев (можно попробовать с блином 5 кг) 3хмакс но не менее 8 раз" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга кривой штанги в наклоне на заднюю дельту 3х15(12)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя на наклонной скамье 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя с уменьшением веса 12-15-18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти с грифом сидя 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда чередуем по разу 12+12 х 3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина хват сверху средний 8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель сидя 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой хват узкий 3х12(10)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга штанги к поясу обратным хватом 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелью стоя в наклоне" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями стоя в наклоне" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+18х3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },



                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра + Гиперэкстензия блин 8 кг 25-20-15-10+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 30-25-20-15-15-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 5х8(10) вес гантель максимум" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП (пресс, вис, планка)" },
        },
                IsPrepared = true, CreatedBy = null },



                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на плечах (глубокий) 25-20-15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания с гирей 22-24 кг 5х20 (можно брать стул для подстраховки)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады с гантелями стоя без смены ног 15-12-10-8 с увеличением веса гантель" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей (также как и на приседе) 5х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х30 (в спокойном темпе)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 50-25-20-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте без смены ног 15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 25-20-15 (вес тела-5 кг-8 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка (на своё усмотрение)" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 200" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 15 кг + Задняя поверхность бедра 15+10 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 5х10 на каждую ногу (7-8-9-10-12 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей 22 кг 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 5х15; планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 25-20-15-10 (вес тела-5-10-15 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады с гантелями стоя без смены ног ( задняя нога на шплинте) 20-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра одной ногой 25-20-15-10 с небольшим увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х30" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, растяжка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на груди 15-12-10-8-6-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Румынская тяга с кривой штангой с увеличением веса 18-15-12-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра одной ногой 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5-8-10кг х 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания у шведской стенки на одной ноге 4х5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра максимальным весом 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 8-10-12-15 (начинаем с самых тяжелых гантель)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 30-35-40-45-50 х 15-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед Зерчера (возьмите с собой каждый по полотенцу) 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра одной ногой без отдыха 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей 24-28 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры с гантелями стоя 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Вис 1’, пресс на наклонной скамье с поворотами 3х20, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед Зерчера (берём полотенце) 15-12-10-8 с увеличением веса (можно взять гриф 13,5 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 10-12-15-20-25 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей 28 кг 3х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине косые мышцы 3х15, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на плечах (глубокий) 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед Зерчера (берём полотенце) 15(гриф)-12-10-8-6-15(гриф) с увеличением веса (можно взять гриф 13,5 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады со штангой без смены ног 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 10-12-15-20-25 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине косые мышцы 3х15, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 25-20-15-12-10-8 (вес подбираем самостоятельно)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 15-12-10 кг х 10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей 26-28 кг 25-20-15 40’’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 4х15, планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед Зерчера 15-12-10-8-6-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 40-45-50-55-60 кг х 15-12-10-8-2х6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине косые мышцы 3х15; планка боковая 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 2х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на плечах (глубокий) 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+10 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями с увеличением веса 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с олимпийским грифом 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хmax" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, вис" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания штанга на плечах на стул 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Мертвая тяга со штангой 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хmax" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 5х20 1’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка боковая 40’’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 25-20-15-10 (не убирайте блины - понадобятся позже)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед Зерчера 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте 3х8 без отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 10-15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике (велосипед + скручивания) 20+20 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1,5’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания со штангой на плечах 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Мертвая тяга со штангой-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 10-15-20-25 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка, икры" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 25-20-15-10 (и не убирай свои блины)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на груди 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 10-15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М. тяга штанга с пола 4х8 (предмаксимальный стартовый вес ~ ну наверное 60 кг)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра с увеличением веса 25-20-15-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Румынская тяга на Т-грифе 20-18-15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры со штангой 3хмах 40(50) кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 25-20-15-10 (блины оставляем на месте)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 15-12-10 кг х 2х8-10-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 10-15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, ПВП" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями с увеличением веса 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед с блином 10-8-5 кг с упором в лежак 10-15-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра одной ногой 3х10 без отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажёре 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "25-20-15-10 + 10-15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 25-20-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 3х15 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "(оба движения начинаем с максимальными весами и уменьшаем) 8-10-12-15-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга с гантелями на одной ноге 12-10-8 кг x 15-18-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 8-10-12-15-18-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике: пулловер + подъемы ног" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "20+10 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5-8-10 кг Х 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания с гантелью 20-15-12 кг под пятки блины по 5 кг 10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 5х8 вес максимальный" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 40-45-50-55-60 кг 15-12-20-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5 кг 2х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на груди 2х10; 2х8; 2х6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра с уменьшением веса 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 250" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания со штангой на плечах 15-2х8-2х6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга штанга с пола 15-3х8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, планка, вис, пресс" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра с увеличением веса 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания с гирей 22 кг (максимально низко как можете)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей 28 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "25+25 3 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры с гантелями 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Задняя поверхность бедра с увеличением веса 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания Зерчера 15-12-10-8-6" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры с гантелями 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед штанга на груди 8-8-10-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 4х15 с рабочим весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады со штангой на месте без смены ног 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед Зерчера 6-8-10-10-12-15 с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 3х8 с большим весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра одной ногой 4х8 без отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями с уменьшением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "6-8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Велосипед 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 15-4х8-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой-//-//-" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 3х10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания со штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "чередуем шаг + 2 приседа 3х10 шагов" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике (велосипед) 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на брусьях 20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 25-20-15-12-10-8 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 5-7-9 кг х 18-15-12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажёре 3хmax" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Прогибы в боковой планке 3х10 на каждую сторону" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 3х15 с весом выше среднего" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед со штангой 15-12-10-8-6 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга штанги с пола 4х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажере 3х25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 2х18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой (средний темный гриф из угла) 12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Мостик на одной ноге с упором на лежак 3х12(15)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра с увеличением веса 4х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 2х20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 25-20-18-15-12-10 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с тяжелыми гантелями 3х12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя п-ов бедра начиная с максимального веса и снижая его 8-10-12-15-18-20-25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажёре 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания со штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга с гирей 32 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15-12-10-8 + 20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя п-ов бедра с уменьшением веса 8-10-12-15-18-20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры в тренажёре 3хmax" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия горизонтальная блин 5 кг со статической задержкой 15 + 10’’ х 3" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания со штангой на стул 18-15-12-10-8-8-5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 3х8(10)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "М.тяга со штангой 3х15 со средним весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хmax" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 15-12-10-3х8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады со штангой без смены ног поочерёдно 8/10/12/15 (допускается делить вес. к примеру, 8 на 4-4-4-4 поочерёдно, 10 на 5-5-5-5 и т.д.)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра с уменьшением веса 8-10-12-2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра с увеличением веса 20-18-15-12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой 15-12-10-8-6 с увеличением веса" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим ногами 3х15 с хорошим рабочим весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры 3хмакс" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия горизонтальная блин 5 кг 3х18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания со штангой с уменьшением веса 2-4-6-8-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады со штангой без смены ног 8-10-12-15 (можно делать разбивку на 4-5-6-3 по 5 каждый из подходов)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Передняя поверхность бедра 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 8 кг 3х18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед с гантелью 25(3х15)-20(18)-15(20) кг под пяточки блин" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады с гантелями на месте шаг назад попеременно 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Мостик с упором туловища на лежак 3х15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Икры, пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч, коленок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Выпады в Смитте" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно (25-20-15-10)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок (-//-//-)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "всё остальное 15;12;10;8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на перекладине 4х15; планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди широким хватом 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед с гирей 16 кг 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя попеременно 5(6) кг 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от лежака 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка коленок 2х15, плеч с гантелями 2 кг 10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 15-12-10 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 12-10-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями 15-12-10 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя одновременно (один вес на все подходы)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя (-//-//-)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Круги по 10;12;15 поповторений" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике локоть к коленке наискосок 4х15 в каждую сторону; Планка 1,5’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 3 кг 10-10-10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия блин 5-8-10 кг х 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 6 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 4 кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Подтягивание широким хватом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола узкий хват локти вдоль тела" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+10+(?) сколько каждый сможет (можно с резиной)+15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "4 серии" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя 8(10) кг" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха сидя с большим весом" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "15+15 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике 4х20 40’’ отдыха" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от брусьев 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Подтягивание на перекладине с резиной 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола локти вдоль туловища 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "4 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка стоя полная амплитуда 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания в Смите 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом (20-25 кг) 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Скакалка 150" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Велосипед 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Подъёмы ног 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Подтягивание на перекладине с резиной широким хватом 15-12-10-8 (можно 12-10-8-8)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола 25-20-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно по 5 раз на руку 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание на брусьях max" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина хват сверху средний 10(8)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола локти вдоль туловища 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя попеременно 12 (берите большой вес)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "4 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа поочерёдно по 3 раза на руку 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот стоя одновременно+поочерёдно 10+5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Кисти штанга сидя 3хмах" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя чередуем по 3 раза 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина широким хватом 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга Т-грифа 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя поочерёдно 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя попеременно 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина с резиной хват сверху широкий 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимание от пола локти вдоль туловища 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Приседания с гирей 22 кг 20" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя поочерёдно 10+5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от лежака 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Велосипед 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Гиперэкстензия (все круги по 15 раз)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель лёжа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим гантель стоя 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина с резиной (все круги по 6 раз, хват широкий)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Присед с гирей (16-18-20 кг) 25-20-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя (можно заменить на штангу) 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Франц.жим стоя с одной гантелью 18" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс на коврике 25" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Планка 1’" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 10-10-10 (жим-разводка-наклоны)" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Перекладина с резиной 8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Отжимания от пола с блином 5(8) кг 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "4 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Молот с одной гантелью в наклоне" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс-блок" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "12+15 х 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями 2 кг 15-15-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Протяжка с гантелью + Жим гантели 15+15 х 3 на каждую руку" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "10+10 3 круга, чередуем по разу" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Тяга вертикальная за голову с уменьшением веса 8-10-12-15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой 5-5-5" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Горизонтальная гиперэкстензия блин 5 кг 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Обведение гири ногами 10" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга без отдыха" },
        },
                IsPrepared = true, CreatedBy = null },

                new PlanEntity {
                    Id = Guid.NewGuid(),
                    Exercises = {
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Жим штанги лёжа в Смите 15-12-10-8-8" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель лёжа" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пуловер поперек скамьи" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+12 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Разводка гантель стоя" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "-//-//- Разводка гантель стоя полная амплитуда" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "8+8 (чередуем по разу) 3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Бицепс-кривуха хват сверху 12" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "+" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Трицепс от грифа 15" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "3 круга" },
            new ExerciseEntity { Id = Guid.NewGuid(), Name = "Пресс, планка" },
        },
                IsPrepared = true, CreatedBy = null }
            );
            context.SaveChanges();
        }
    }

    public static void SeedExercisesData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlansDbContext>();

        if (!context.Exercises.Any())
        {
            context.AddRange(
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Бабочка", MuscleGroup = "", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с гантелью стоя в наклоне", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя на наклонной скамье", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с гантелями сидя одновременно", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя в упоре на конструкцию тренажёра Смит",
                    MuscleGroup = "Бицепсы", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя поочерёдно", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с гантелями стоя", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с грифом в наклоне", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой на скамье Скотта", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой сидя", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой стоя", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой хват сверху", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с кривой штангой", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с прямой штангой", MuscleGroup = "Бицепсы", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом на скамье Скотта", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом с расширителями", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Бицепс с прямым грифом хват сверху", MuscleGroup = "Бицепсы",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Вис на перекладине", MuscleGroup = "Вис", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Выпады в Смитте", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Выпады с гантелями на месте шаг назад попеременно", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Выпады с гантелями стоя без смены ног", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Выпады с гантелями стоя без смены ног (задняя нога на шплинте)",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Выпады со штангой на месте без смены ног поочерёдно", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Выпады со штангой на месте без смены ног", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Гиперэкстензия горизонтальная со статической задержкой",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Гиперэкстензия горизонтальная", MuscleGroup = "Нижняя часть спины", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Гиперэкстензия", MuscleGroup = "Нижняя часть спины", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантели", MuscleGroup = "Грудь", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель лёжа на наклонной скамье", MuscleGroup = "Грудь",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель лёжа поочерёдно", MuscleGroup = "Грудь", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель лёжа с отрицательным наклоном", MuscleGroup = "Грудь",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель лёжа", MuscleGroup = "Грудь", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель сидя в упоре", MuscleGroup = "Плечи", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель сидя", MuscleGroup = "Плечи", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель стоя попеременно", MuscleGroup = "Плечи", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим гантель стоя", MuscleGroup = "Плечи", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Жим ногами", MuscleGroup = "", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Жим Свенда", MuscleGroup = "", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги лёжа в Смите", MuscleGroup = "Грудь", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги лёжа на горизонтальной скамье", MuscleGroup = "Грудь",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги лёжа на наклонной скамье", MuscleGroup = "Грудь",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги лёжа узким хватом", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги лёжа широким хватом", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги лёжа", MuscleGroup = "Грудь", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги стоя за голову", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Жим штанги стоя", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Задняя дельта в Смите", MuscleGroup = "Задняя дельта", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Задняя поверхность бедра", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Икры в тренажере", MuscleGroup = "Икроножные", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Икры с гантелями", MuscleGroup = "Икроножные", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Икры со штангой", MuscleGroup = "Икроножные", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Икры", MuscleGroup = "Икроножные", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Кисти с грифом сидя", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Кисти штанга хват сверху", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Мёртвая тяга с гирей", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Мёртвая тяга штанга с пола", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Молот с блином", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Молот с одной гантелью в наклоне", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Молот сидя поочерёдно", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Молот сидя с упором спиной поочередно", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Молот стоя в наклоне одной рукой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Молот стоя попеременно", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Молот стоя", MuscleGroup = "", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Мостик на одной ноге с упором на лежак", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Мостик с упором туловища на лежак", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Обведение гири ногами", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отведение руки в блоке", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отведение руки в кроссовере", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отведение руки в тренажере на заднюю дельту", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отведение руки лёжа с упором на скамью", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отведение руки лёжа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отведение руки назад лёжа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отжимание на брусьях", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отжимание от лежака широким хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отжимание от пола с блином", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отжимание от пола узкий хват локти вдоль тела", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отжимание от пола", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Отжимания от лежака узким хватом", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Передняя поверхность бедра одной ногой", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Передняя поверхность бедра", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Перекладина хват параллельный", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Перекладина хват сверху средний", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Перекладина хват сверху широкий", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Перекладина хват снизу узкий", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Планка боковая", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Планка на одной ноге", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Планка на прямых руках", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Планка", MuscleGroup = "", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Подъёмы ног", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс велосипед", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на брусьях", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на коврике (велосипед + скручивания)", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на коврике (пулловер + подъемы ног)", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на коврике локоть к коленке наискосок", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на коврике", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на наклонной скамье с поворотами с набивным мячом",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на наклонной скамье с поворотами", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на перекладине (косые мышцы)", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пресс на перекладине", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед Зерчера", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед с блином с упором в лежак", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед с блином", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед с гантелью под пятки блин", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед с гирей", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед со штангой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед штанга на груди", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Присед штанга на плечах (глубокий)", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Приседания в Смите", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Приседания со штангой на стул", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Приседания у шведской стенки на одной ноге", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Приседания штанга на плечах", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Приседания штанга на плечах на стул", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Прогибы в боковой планке на каждую сторону", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Протяжка с гантелью", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой (широкий, средний, узкий хваты)",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой узким хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой широким хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Протяжка с кривой штангой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Протяжка с прямым грифом средним хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Пуловер с гантелью лёжа поперёк скамьи", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка блинов стоя в наклоне", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель в наклоне", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель в упоре на лежак", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье лицом к полу",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье полуамплитуда",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель лёжа на наклонной скамье", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель лёжа полуамплитуда", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель лёжа с упором на лежак", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель лёжа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель сидя полная амплитуда", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель сидя", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель стоя одной рукой в наклоне и упоре", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель стоя полная амплитуда", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель стоя поочерёдно", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гантель стоя", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка гирь лёжа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разводка стоя полная амплитуда ладони наружу", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разминка коленок", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разминка плеч с гантелями (жим-разводка-наклоны)", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Разминка плеч", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Румынская тяга на Т-грифе", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Румынская тяга с кривой штангой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Сведение рук в кроссовере", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Сведение рук стоя в тренажере", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Сведение рук стоя", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Сгибание Зотмана", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                    { Id = Guid.NewGuid(), Name = "Скакалка", MuscleGroup = "", IsPrepared = true, CreatedBy = null },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс от грифа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс от лежака", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс с гантелью одной рукой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс с гантелью стоя", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс с гантелями лёжа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс с гантелями стоя в наклоне", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс с кривой штангой лёжа", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс-блок", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс-блок одной рукой обратным хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Трицепс-блок одной рукой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Турецкие подтягивания", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель в наклоне", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель лёжа на наклонной скамье лицом к полу",
                    MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель с упором лба на лежак", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель с упором на лежак", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель стоя в наклоне", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга 2-ух гантель стоя одновременно", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга в Смите за спиной на заднюю дельту", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная за голову", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди обратным хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди средним хватом сверху", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват обратный", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху средний", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху широкий", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди хват сверху", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди широким хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга вертикальная к груди", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга гантели в наклоне", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга кривой штанги в наклоне на заднюю дельту", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга на заднюю дельту в Смите", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга прямой штанги на заднюю дельту", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга с гантелями на одной ноге", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга Т-грифа параллельным хватом", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга Т-грифа", MuscleGroup = "", IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга штанги в наклоне обратным хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга штанги к поясу обратным хватом", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Тяга штанги к поясу", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Французский жим лёжа с кривой штангой", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Французский жим стоя с одной гантелью", MuscleGroup = "",
                    IsPrepared = true, CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Шаги-фермера с гантелями", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Шаги-фермера со штангой", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                },
                new ExerciseEntity
                {
                    Id = Guid.NewGuid(), Name = "Шраги с гантелями", MuscleGroup = "", IsPrepared = true,
                    CreatedBy = null
                });
        }

        context.SaveChanges();
    }
}

