insert into dbo.ShippingSurcharges
select sr.RateID,'ISL',null
from dbo.ShippingRates sr
join dbo.ShippingRoutes rout on rout.RouteID=sr.RouteID
join dbo.PortsAndPlaces o on o.ID = rout.Origin
join dbo.PortsAndPlaces d on d.ID = rout.Destination
where o.Code in ('USORF','USNYC','USSAV')
and d.Name='JUBAIL'


