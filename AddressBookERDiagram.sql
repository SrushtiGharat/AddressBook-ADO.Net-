Create table AddressBook
(
BookId varchar(5) primary key,
BookName varchar(10) not null,
Type varchar(10) not null
);
Insert into AddressBook values
('B01','Book1','Friends'),
('B02','Book2','Family'),
('B03','Book3','Profession');


Create table Contact
(
CId int identity(1,1) primary key,
FirstName varchar(15) not null,
LastName varchar(15) not null,
Address varchar(25) not null,
ZipCode varchar(8) not null FOREIGN KEY REFERENCES Zip(ZipCode),
PhoneNo varchar(15) not null,
Email varchar(40) not null
);
Insert into Contact values
('Ram','Singh','23-River Park','400500','9999999999','ramsingh@gmail.com'),
('Ravi','Pandey','17-Orchids Colony','500300','8888888888','ravipandey@gmail.com'),
('Seeta','Yadav','02-Green Tower','400503','6666666666','seetasingh@gmail.com'),
('Riya','Sharma','32-Street Road','300200','444444444','riyasharma@gmail.com'),
('Shyam','Patil','20-Shastri Nagar','500303','7777777777','shyampatil@gmail.com');

Create table AddressBookContact
(
BookId varchar(5) not null FOREIGN KEY REFERENCES AddressBook(BookId),
CId int not null FOREIGN KEY REFERENCES Contact(CId),
);
Insert into AddressBookContact values
('B01',1),
('B01',3),
('B02',2),
('B02',3),
('B03',5),
('B03',4)

Create table Zip
(
ZipCode varchar(8) primary key,
City varchar(25) not null,
State varchar(25) not null
)
Insert into Zip values
('400500','Mumbai','Maharashtra'),
('500300','Bangalore','Karnataka'),
('400503','Mumbai','Maharashtra'),
('300200','Hyderabad','Telangana');

--UC6_Get Contact by city--
Select FirstName,LastName,PhoneNo,Email from Contact INNER JOIN Zip ON Contact.ZipCode = Zip.ZipCode where city = 'Mumbai'

--UC7_Get Count by city and state--
Select state,city,Count(CId) from Contact Inner Join Zip ON Contact.ZipCode = Zip.ZipCode group by city,state

--UC8_Given city sort by name--
Select FirstName,LastName from Contact Inner Join Zip ON Contact.ZipCode = Zip.ZipCode where city = 'Mumbai'

--UC10_Get Count of contact by type--
Select Type,Count(Contact.CId) from Contact Inner Join AddressBookContact ON Contact.CId = AddressBookContact.CId
Inner Join AddressBook ON AddressBook.BookId = AddressBookContact.BookId group by Type;

Select Type,Contact.CId,FirstName,LastName,Address,Contact.ZipCode,PhoneNo,Email,City,State 
from AddressBook Inner JOIN  AddressBookContact On AddressBook.BookId = AddressBookContact.BookId
INNER JOIN Contact ON AddressBookContact.CId = Contact.CId
Inner Join Zip ON Contact.ZipCode = Zip.ZipCode;

Select Type,FirstName,LastName,Address,Contact.ZipCode,PhoneNo,Email,City,State 
                                    from AddressBook Inner JOIN  AddressBookContact On AddressBook.BookId = AddressBookContact.BookId
                                    INNER JOIN Contact ON AddressBookContact.CId = Contact.CId
                                    Inner Join Zip ON Contact.ZipCode = Zip.ZipCode;