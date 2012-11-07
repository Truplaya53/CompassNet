select * from dbo.PortsPlaces where [POR PORT_NAME] like '%delaw%' or [POR STATE]='de'

***
declare @Zip int
set @Zip=31082
while @Zip <= 31082
begin
insert into dbo.PlacesZipcodes values (CONVERT(varchar,@Zip), 91)
set @Zip=@Zip+1
end


--insert into dbo.PlacesZipcodes select z.zip,p.id from USZipcodes z
--join portsandplaces p on z.primary_city=p.city and p.[state]='GA'
--where zip between '30300%' and '31000%' 


select z.zip,p.id from USZipcodes z
join portsandplaces p on z.primary_city=p.city and p.[state]='GA'
where zip between '30300%' and '31000%' 



select name, COUNT(*) from PortsandPlaces
group by name having COUNT(*) > 1