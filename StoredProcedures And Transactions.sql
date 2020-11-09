create or alter procedure SpUpdateDetails
(
@FirstName varchar(25),
@LastName varchar(25),
@PhoneNo varchar(25)
)
as
begin
Update Contact set PhoneNo = @PhoneNo where FirstName = @FirstName and LastName = @LastName
end

create or alter procedure SpGetContactsByDateRange
(
@StartDate date,
@EndDate date
)
as
begin

Select Type,FirstName,LastName,Address,Contact.ZipCode,PhoneNo,Email,City,State,Date_Added 
from AddressBook Inner JOIN  AddressBookContact On AddressBook.BookId = AddressBookContact.BookId
INNER JOIN Contact ON AddressBookContact.CId = Contact.CId
Inner Join Zip ON Contact.ZipCode = Zip.ZipCode
where Date_Added between @StartDate and @EndDate

end
