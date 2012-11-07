--insert into dbo.ShippingSurcharges
select sr.RateID,'Fuel',414,sr.BaseRate
from dbo.ShippingRates sr
join dbo.ShippingZipcodeZones zones on zones.ZoneID=sr.ZoneID
where zones.Name='363'


