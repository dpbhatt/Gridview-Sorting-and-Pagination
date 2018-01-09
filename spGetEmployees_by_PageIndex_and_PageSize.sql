USE [Northwind]
GO

/****** Object:  StoredProcedure [dbo].[spGetEmployees_by_PageIndex_and_PageSize]    Script Date: 1/9/2018 11:03:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[spGetEmployees_by_PageIndex_and_PageSize]
@PageIndex int,    
@PageSize int,
@SortExpression nvarchar(50),
@SortDirection nvarchar(50),
@TotalRows int output  
as    
Begin    
 Declare @StartRowIndex int   
 Declare @EndRowIndex int  
   
 Set @StartRowIndex = (@PageIndex * @PageSize) + 1;  
        Set @EndRowIndex = (@PageIndex + 1) * @PageSize;  
   
 Select EmployeeID, FirstName, LastName, Title, City from    
 (Select ROW_NUMBER() over 
  (
   order by
    case when @SortExpression = 'EmployeeID' and @SortDirection = 'ASC' 
      then EmployeeID end asc, 
    case when @SortExpression = 'EmployeeID' and @SortDirection = 'DESC' 
      then EmployeeID end desc,
    case when @SortExpression = 'FirstName' and @SortDirection = 'ASC' 
      then FirstName end asc, 
    case when @SortExpression = 'FirstName' and @SortDirection = 'DESC' 
      then FirstName end desc,
	case when @SortExpression = 'LastName' and @SortDirection = 'ASC' 
      then LastName end asc, 
    case when @SortExpression = 'LastName' and @SortDirection = 'DESC' 
      then LastName end desc,
    case when @SortExpression = 'Title' and @SortDirection = 'ASC' 
      then Title end asc, 
    case when @SortExpression = 'Title' and @SortDirection = 'DESC' 
      then Title end desc,
    case when @SortExpression = 'City' and @SortDirection = 'ASC' 
      then City end asc, 
    case when @SortExpression = 'City' and @SortDirection = 'DESC' 
      then City end desc
  ) as RowNumber, EmployeeID, FirstName, LastName, Title, City    
 from Employees) Employees    
 Where RowNumber >= @StartRowIndex and RowNumber <= @EndRowIndex  
    
 Select @TotalRows = COUNT(*) from Employees  
End
GO


