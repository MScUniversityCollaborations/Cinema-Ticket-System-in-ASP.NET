create database MovieTickets
go

use MovieTickets
go

create table Users(
id int primary key identity(1,1) NOT NULL,
email nvarchar(max) NOT NULL,
password_hash nvarchar(max) NOT NULL,
name nvarchar(max) NOT NULL,
mobile nvarchar(max),
is_admin bit default(0) NOT NULL,
CONSTRAINT Screening_ak_1 UNIQUE (email, mobile)
)
go

create table Movies(
id int primary key identity(1,1) NOT NULL,
title nvarchar(256) NOT NULL,
description nvarchar(max) NULL,
duration_min int NOT NULL
)
go

create table Screenings(
id int primary key identity(1,1) NOT NULL,
movie_id int references Movies(id) on delete cascade NOT NULL,
auditorium_id int references Auditoriums(id) on delete cascade NOT NULL,
screening_star datetime NOT NULL,
CONSTRAINT Screening_ak_1 UNIQUE (movie_id, auditorium_id, screening_start),
)
go

create table Auditoriums(
auditorium_id int primary key identity(1,1) NOT NULL,
name nvarchar(32) NOT NULL,
seats_no int NOT NULL
)
go

create table Seats(
id int primary key identity(1,1) NOT NULL,
row int NOT NULL,
number int NOT NULL,
auditorium_id int references Auditoriums(id) on delete cascade NOT NULL
)
go

create table Seats_Reserved(
id int primary key identity(1,1) NOT NULL,
seat_id int references Seats(id) on delete cascade NOT NULL,
reservation_id int references Reservations(id) on delete cascade NOT NULL,
screening_id int references Screenings(id) on delete cascade NOT NULL
)
go

create table Reservations(
    id int primary key identity(1,1) NOT NULL,
    screening_id int references Screenings(id) on delete cascade NOT NULL,
    user_id int references Users(id) on delete cascade NOT NULL,
    reserved bit default(0) NOT NULL,
    photo nvarchar(max) NULL,
    paid bit default(0) NOT NULL,
    payment_type nvarchar(max) NOT NULL,
    active bit default(0) NOT NULL
)
go

insert into Users values('admin@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
'Admin', '0000000000', 1)
go
-- password:  admin123

CREATE PROCEDURE getUserReservation
@email nvarchar(max), @mobile nvarchar(max)
AS
    select * 
    from Reservations
    inner join Users on Reservations.user_id = Users.id
    where Users.email = @email OR Users.mobile = @mobile
GO