create database RailDB
use RailDB

create table train(
train_no numeric(5) not null primary key,
train_name varchar (30),
source varchar(20),
destination varchar(20),
)
insert into train values(12530,'PPT Express','Lucknow','Gorakhpur'),
(10390,'Delhi Express','Gorakhpur','Delhi'),
(34500,'Shatabdi exp','Bangalore','Delhi')

-- adding isActive to Train_details table
-- And Implementing SOFT Delete...
alter table train add isActive varchar(10) not null default 'Active'





--Class/Seat Available
create table class_Avail(
SerialNo int identity,
train_no numeric(5) foreign key references train(train_no),
[1-AC-seat-Avail] int,
[2-AC-seat-Avail] int,
[SL-seat-Avail] int,
)
insert into class_Avail values(12530,100,200,300),(10390,100,200,300),(34500,100,200,300)
delete from class_Avail where train_no=14384
select * from class_Avail


--Fare details
create table fare(
SequenceNo int identity,
train_no numeric(5) foreign key references train(train_no),
[1-AC-fare] int,
[2-AC-fare] int,
SL_fare int,
)

insert into fare values(12530,3200,2100,1000),(10390,5000,3000,1500),(34500,4500,2200,1100);
select * from fare
delete from fare where train_no=14384


--User Table
create table user_details(
[user-id] int primary key,
[user-name] varchar (20),
) 
-- adding password to user_details table
alter table user_details add [Password] varchar(10) not null



-- Admin details
create table admin(
[admin-id] int primary key,
[Admin-name] varchar (20),
[admin-password] varchar(20),
)

insert into admin values(123,'Avinash','@Avi123')


--- Booked Tickets
create table bookTicket(
[Book-Id] numeric(10) primary key,
[user-id] int foreign key references user_details([user-id]),
train_no numeric(5) foreign key references train(train_no),
[total-fare] float,
[Booking-Date-Time] date,
)
-- adding attributes....
alter table bookTicket add Class_type varchar(10) not null
alter table bookTicket add NumberTickets int not null
alter table bookTicket add [Status] varchar(10) not null default 'Booked'



-- Cancelled Tickets
create table cancelTicket(
[Cancel-Id] numeric(10) primary key,
[Book-Id] numeric(10) foreign key references bookTicket([Book-Id]),
[user-id] int foreign key references user_details([user-id]),
train_no numeric(5) foreign key references train(train_no),
[Cancelled-date-time]  date,
[Refund-Amount] float,)





-- stored procedure 
CREATE or alter PROCEDURE UpdateBookedTicket( @TrainNo NUMERIC(5),
    @Class NVARCHAR(20),
    @SeatsBooked INT)
   
AS
BEGIN

    IF @Class = '1AC'
        UPDATE class_Avail
        SET [1-AC-seat-Avail] = [1-AC-seat-Avail] - @SeatsBooked
        WHERE train_no = @TrainNo;
    ELSE IF @Class = '2AC'
        UPDATE class_Avail
        SET [2-AC-seat-Avail] = [2-AC-seat-Avail] - @SeatsBooked
        WHERE train_no = @TrainNo;
    ELSE IF @Class = 'SL'
        UPDATE class_Avail
        SET [SL-seat-Avail] = [SL-seat-Avail] - @SeatsBooked
        WHERE train_no = @TrainNo;
END


--For Cancel Ticket
CREATE or alter PROCEDURE UpdateCancelTicket( @TrainNo NUMERIC(5),
    @Class NVARCHAR(20),
    @SeatsBooked INT)
   
AS
BEGIN

    IF @Class = '1AC'
        UPDATE class_Avail
        SET [1-AC-seat-Avail] = [1-AC-seat-Avail] + @SeatsBooked
        WHERE train_no = @TrainNo;
    ELSE IF @Class = '2AC'
        UPDATE class_Avail
        SET [2-AC-seat-Avail] = [2-AC-seat-Avail] + @SeatsBooked
        WHERE train_no = @TrainNo;
    ELSE IF @Class = 'SL'
        UPDATE class_Avail
        SET [SL-seat-Avail] = [SL-seat-Avail] + @SeatsBooked
        WHERE train_no = @TrainNo;
END

-- procedure to add seats and of new train
CREATE or alter PROCEDURE AddclassSeats( 
@TrainNo NUMERIC(5),
@firstAcSeats int,
@SecAcSeats int,
@SLSeats int
)
AS
insert into class_Avail(train_no,[1-AC-seat-Avail],[2-AC-seat-Avail],[SL-seat-Avail]) values(@TrainNo,@firstAcSeats,@SecAcSeats,@SLSeats)

AddclassSeats 12531,250,150,100

-- proceudure to add fair of 1ac,2ac and SL class
CREATE or alter PROCEDURE AddclassFair( 
@TrainNo NUMERIC(5),
@firstAcSeatsfare int,
@SecAcSeatsfare int,
@SLSeatsfare int
)
AS
insert into fare (train_no,[1-AC-fare],[2-AC-fare],SL_fare) values(@TrainNo,@firstAcSeatsfare,@SecAcSeatsfare,@SLSeatsfare)


------------------------------------
select * from train
select * from fare
select * from class_Avail
select * from user_details
select * from admin
select * from cancelTicket
select * from bookTicket