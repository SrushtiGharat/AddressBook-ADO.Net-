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