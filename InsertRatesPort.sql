--***

insert into dbo.ShippingRates
select distinct 1,r.RouteID, null,c.ID,com.CommodityTypeID,1399,'9/1/2012','11/30/2012'
from dbo.ContainerTypes c,dbo.CommodityTypes com, dbo.ShippingRoutes r
join dbo.PortsAndPlaces o on o.ID=r.Origin
join dbo.PortsAndPlaces d on d.ID=r.Destination
where o.Code in ('USORF','USNYC','USSAV')
and d.Name ='JUBAIL'
and com.Name in ('Appliances','Building Materials','Household Goods or Personal Effects','Appliances - Airconditioning')
and (c.Name = 
--'20'' DC')
--'20'' HC'
'40'' DC' or c.Name = '40'' HC')
