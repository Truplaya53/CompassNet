select * from USZipcodes where zip like '3%'
order by zip

select z.zipcode,p.name from PlacesZipcodes z 
join PortsandPlaces p on  z.PlaceID=p.ID
where zipcode like '3%' order by zipcode


insert into dbo.ShippingRoutes
select distinct z.PlaceId,1,'ALL MOTOR' from PlacesZipcodes z 
join PortsandPlaces p on  z.PlaceID=p.ID
where zipcode between '30017%' and '390%'

select * from (
select z.Zipcode,po.Name Origin,pd.Name Destination, po.Code from ShippingRoutes r
join PlacesZipcodes z on z.PlaceID = r.origin
join PortsAndPlaces po on po.Id = r.origin
join PortsAndPlaces pd on pd.Id = r.Destination
union
select isnull(z.code,'00000') CODE,po.Name Origin,pd.Name Destination, po.Code from ShippingRoutes r
left join PortsAndPlaces z on z.ID = r.origin
join PortsAndPlaces po on po.Id = r.origin
join PortsAndPlaces pd on pd.Id = r.Destination
) SRoutes 
order by Origin, Code


select * from (
select z.Zipcode CODE,po.Name Origin, po.ID from ShippingRoutes r
join PlacesZipcodes z on z.PlaceID = r.origin
join PortsAndPlaces po on po.Id = r.origin
join PortsAndPlaces pd on pd.Id = r.Destination
union
select isnull(z.code,'00000') CODE,po.Name Origin, po.ID from ShippingRoutes r
left join PortsAndPlaces z on z.ID = r.origin
join PortsAndPlaces po on po.Id = r.origin
join PortsAndPlaces pd on pd.Id = r.Destination
) SRoutes 
order by Origin, Code

select ORIGIN,destination,transportMode, COUNT(*) from ShippingRoutes
group by ORIGIN,destination,transportMode
