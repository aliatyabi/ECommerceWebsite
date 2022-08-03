INSERT INTO Products
           ([CategoryId]
           ,[SubCategoryId]
           ,[Title]
           ,[Article]
           ,[ShortDescription]
           ,[Text]
           ,[ImageName]
           ,[Fee]
           ,[DiscountPercent]
           ,[CreatedDate]
           ,[FurtherSubCategoryId]
           ,[FurthestSubCategoryId]
           ,[Status])

select case Gender
when 'MENS' then 191
when 'WOMENS' then 190
when 'KIDS' then 189
when 'UNISEX' then 191
end,
case Division
when 'FOOTWEAR' then 192
when 'ACCESSORIES' then 250
when 'EQUIPMENT' then 250
end,
[Meta Title],Article,[Meta Description],[Description],
Article + '_',Price,0,GETDATE(),195,201,1
from adidas

     
go    
     
     
update Products
set ImageName = ImageName + cast(ProductId as varchar(10)) + '.jpg'
     

go

update b
set b.Price = a.Cost
from [Adidas-srv].[Fit].[dbo].[Goods] a
inner join adidas b
on a.Spec_Code = b.Article collate Arabic_CI_AI

