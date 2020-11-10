--Update Details--
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

--Get Contact By Date Range--
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

--Get Count By City And State--
create or alter procedure SpGetCountByCityState
as
begin
Select state,city,Count(CId) from Contact Inner Join Zip ON Contact.ZipCode = Zip.ZipCode group by city,state
end

--Add Contact Details--
create or alter procedure SpAddContactDetails
(
@FirstName varchar(25),
@LastName varchar(25),
@Address varchar(50),
@ZipCode varchar(10),
@City varchar(20),
@State varchar(20),
@PhoneNo varchar(15),
@Email varchar(25),
@Type varchar(15),
@Date date,
@CId int out
)
as
begin
set XACT_ABORT on;
begin try
begin TRANSACTION;

IF(select ZipCode from Zip Where ZipCode = @ZipCode)=0
BEGIN
insert into Zip values
(
@ZipCode,@City,@State
)
END

insert into Contact values
(
@FirstName,@LastName,@ZipCode,@PhoneNo,@Email,@Address,@Date
)
set @CId = SCOPE_IDENTITY()

insert into AddressBookContact values
(
(select BookId from AddressBook where Type = @Type),@CId
)

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
select ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
IF(XACT_STATE())=-1
BEGIN
  PRINT N'The transaction is in an uncommitable state.'+'Rolling back transaction.'
  ROLLBACK TRANSACTION;
  END;

  IF(XACT_STATE())=1
  BEGIN
    PRINT 
	    N'The transaction is committable. '+'Committing transaction.'
       COMMIT TRANSACTION;
	END;
	END CATCH
END
