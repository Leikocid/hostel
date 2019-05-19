SET QUOTED_IDENTIFIER OFF;
GO
USE [hostel];
GO
declare @IdentityOutput table ( ID int );
declare @id INT;

-----------------

delete from [dbo].[UserSet];
delete from [dbo].[PersonSet];
delete from [dbo].[FacultySet];
delete from [dbo].[HostelSet];

-- создание 2х пользователей системы

insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) output inserted.Id into @IdentityOutput values ('Элон', 'Маск', 'Рамуальдович');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;
insert [dbo].[UserSet] (Login, Pasword, Active, Person_Id) values ('q', 'q', 'TRUE', @id);

insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) output inserted.Id into @IdentityOutput values ('Филлип', 'Киркоров', 'Бедросович');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;
insert [dbo].[UserSet] (Login, Pasword, Active, Person_Id) values ('a', 'a', 'TRUE', @id);

-- создание факультетов и групп
insert [dbo].[FacultySet] (Name) output inserted.Id into @IdentityOutput values ('Факультет технологий управления и гуманитаризации');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 6, @id);

insert [dbo].[FacultySet] (Name) output inserted.Id into @IdentityOutput values ('Энергетичесий факультет');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 6, @id);

insert [dbo].[FacultySet] (Name) output inserted.Id into @IdentityOutput values ('Факультет информационных технологий');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (1, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (2, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (3, 6, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 1, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 2, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 3, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 4, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 5, @id);
insert [dbo].[GroupSet] (StudyYear, Number, Faculty_Id) values (4, 6, @id);

-- создание общежитий и комнат
insert [dbo].[HostelSet] (Name, Address) output inserted.Id into @IdentityOutput values ('Пятерка', 'г. Минск, ул. Беларуская, 21');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;

with numbers as (select 1 as n union all select n + 1 from numbers where n < 20)
insert into [dbo].[RoomSet] (Floor, Number, Capacity, Hostel_Id) select n1.n, n1.n*100 + n2.n, 5, @id from numbers n2, numbers n1
where n1.n <= 15 and n2.n <= 12; 

insert [dbo].[HostelSet] (Name, Address) output inserted.Id into @IdentityOutput values ('Гурского', 'г. Минск, ул. Гурского, 7');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;

with numbers as (select 1 as n union all select n + 1 from numbers where n < 20)
insert into [dbo].[RoomSet] (Floor, Number, Capacity, Hostel_Id) select n1.n, n1.n*100 + n2.n, 4, @id from numbers n2, numbers n1
where n1.n <= 5 and n2.n <= 12;

insert [dbo].[HostelSet] (Name, Address) output inserted.Id into @IdentityOutput values ('Копейка', 'г. Минск, ул. Бобруйская, 19');
select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;

with numbers as (select 1 as n union all select n + 1 from numbers where n < 20)
insert into [dbo].[RoomSet] (Floor, Number, Capacity, Hostel_Id) select n1.n, n1.n*100 + n2.n, 3, @id from numbers n2, numbers n1
where n1.n <= 3 and n2.n <= 6; 

-- создание персон
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Юрий', 'Алексеевич', 'Гагарин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Герман', 'Степанович', 'Титов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Андриян', 'Григорьевич', 'Николаев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Павел', 'Романович', 'Попович');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валерий', 'Фёдорович', 'Быковский');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валентина', 'Владимировна', 'Терешкова');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Михайлович', 'Комаров');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Константин', 'Петрович', 'Феоктистов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Борис', 'Борисович', 'Егоров');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Павел', 'Иванович', 'Беляев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Алексей', 'Архипович', 'Леонов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Георгий', 'Тимофеевич', 'Береговой');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Александрович', 'Шаталов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Борис', 'Валентинович', 'Волынов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Алексей', 'Станиславович', 'Елисеев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Евгений', 'Васильевич', 'Хрунов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Георгий', 'Степанович', 'Шонин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валерий', 'Николаевич', 'Кубасов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Анатолий', 'Васильевич', 'Филипченко');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владислав', 'Николаевич', 'Волков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Виктор', 'Васильевич', 'Горбатко');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Виталий', 'Иванович', 'Севастьянов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Николай', 'Николаевич', 'Рукавишников');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Георгий', 'Тимофеевич', 'Добровольский');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Виктор', 'Иванович', 'Пацаев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Василий', 'Григорьевич', 'Лазарев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Олег', 'Григорьевич', 'Макаров');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Пётр', 'Ильич', 'Климук');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валентин', 'Витальевич', 'Лебедев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Юрий', 'Петрович', 'Артюхин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Геннадий', 'Васильевич', 'Сарафанов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Лев', 'Степанович', 'Дёмин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Алексей', 'Александрович', 'Губарев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Георгий', 'Михайлович', 'Гречко');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Виталий', 'Михайлович', 'Жолобов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Викторович', 'Аксёнов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Вячеслав', 'Дмитриевич', 'Зудов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валерий', 'Ильич', 'Рождественский');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Юрий', 'Николаевич', 'Глазков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Васильевич', 'Ковалёнок');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валерий', 'Викторович', 'Рюмин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Юрий', 'Викторович', 'Романенко');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Александрович', 'Джанибеков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Сергеевич', 'Иванченков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Афанасьевич', 'Ляхов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Леонид', 'Иванович', 'Попов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Юрий', 'Васильевич', 'Малышев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Леонид', 'Денисович', 'Кизим');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Геннадий', 'Михайлович', 'Стрекалов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Виктор', 'Петрович', 'Савиных');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Анатолий', 'Николаевич', 'Березовой');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Александрович', 'Серебров');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Светлана', 'Евгеньевна', 'Савицкая');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Георгиевич', 'Титов');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Павлович', 'Александров');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Алексеевич', 'Соловьёв');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Олег', 'Юрьевич', 'Атьков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Игорь', 'Петрович', 'Волк');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Владимир', 'Владимирович', 'Васютин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Александрович', 'Волков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Иванович', 'Лавейкин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Степанович', 'Викторенко');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Муса', 'Хираманович', 'Манаров');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Анатолий', 'Семёнович', 'Левченко');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Анатолий', 'Яковлевич', 'Соловьёв');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Валерий', 'Владимирович', 'Поляков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Сергей', 'Константинович', 'Крикалёв');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Александр', 'Николаевич', 'Баландин');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Геннадий', 'Михайлович', 'Манаков');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Виктор', 'Михайлович', 'Афанасьев');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Анатолий', 'Павлович', 'Арцебарский');
insert [dbo].[PersonSet] (FirstName, LastName, MiddleName) values ('Токтар', 'Онгарбаевич', 'Аубакиров');

-- все персоны становятся студентами 
insert into [dbo].[StudentSet] (Active, Person_Id, Group_Id)
select 'true', p.Id, g.Id from (
select ROW_NUMBER() OVER(ORDER BY Id ASC) AS Row, * from [dbo].[PersonSet]
) as p inner join (
select ROW_NUMBER() OVER(ORDER BY Id DESC) AS Row, * from [dbo].[GroupSet]
) as g on p.Row = g.Row where p.Row > 2;

-- заселяем их последовательно в каждую вторую комнату
insert into [dbo].[OcupationSet] (FromDate, Room_Id, Student_Id)
select GETDATE(), r.Id, s.Id from (
select ROW_NUMBER() OVER(ORDER BY Id ASC) AS Row, * from [dbo].[StudentSet]
) as s inner join (
select ROW_NUMBER() OVER(ORDER BY Id DESC) AS Row, * from [dbo].[RoomSet]
) as r on s.Row * 2 = r.Row;



