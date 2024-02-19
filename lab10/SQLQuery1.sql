create database lab10
drop database lab10

CREATE TABLE ACCOUNT(
	NumAcc int primary key,
	typeOfDeposit varchar(40),
	dateOfOpening date,
	OwnerAcc varchar(100) foreign key references OwnerOfAccount(AccOwner)
)

CREATE TABLE OwnerOfAccount (
	AccOwner varchar(100) primary key,
	OwnerBirth date,
	Passport varchar(20),
	Pic varchar(200)
)

INSERT INTO OwnerOfAccount (AccOwner, OwnerBirth, Passport, pic)
	VALUES ('Буданова Ксения Андреевна','2003-07-10','AB3176888', 'e:\1POIT\2\OOP\labs\lab10\lab10\pics\2.jpg'),
		   ('Николаева Евгения Владимировна','2002-07-17','AB2374248', 'e:\1POIT\2\OOP\labs\lab10\lab10\pics\1.jpg'),
		   ('Гуд Владислав Евгеньевич','2002-08-13','AB3274824', 'e:\1POIT\2\OOP\labs\lab10\lab10\pics\3.jpg')
INSERT INTO ACCOUNT (NumAcc, typeOfDeposit, dateOfOpening, OwnerAcc)
	VALUES(2734824, 'Валютный счёт', '2018-03-29', 'Гуд Владислав Евгеньевич')


select*from ACCOUNT;

create procedure PROCed
as 
begin 
declare @k int = (select count (*) from ACCOUNT)
select * from ACCOUNT;
return @k;
end;

declare @kk int = 0;
exec @kk = PROCed;
print 'PROCEDURE = ' + cast (@kk as varchar(3));