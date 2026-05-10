select o.OwnerName,h.HouseID,h.City,h.Address
from [Owner] o join House h
on h.OwnerID = o.OwnerID
where OwnerName='Ahmed Hassan';


select HeaterID,HeaterType,power,Efficiency,IsActive
from Heater
where HouseID=1;


select * from DailyUsage
where WorkHours>8;


select HouseID,ReportMonth,TotalworkingHours,MonthlyAverageCost
from MonthlyReport
where HouseID=5;


select h.HouseID,h.City,h.Address,mr.MonthlyTotalCost
from House h join MonthlyReport mr
on h.HouseID = mr.HouseID
order by MonthlyTotalCost desc;


select * from Heater order by Power ;
select * from Heater order by Power desc ;


--!!
select h.HouseID , h.City, h.Address,mr.ReportMonth, mr.TotalWorkingHours as TotalWorkingHoursOFTheHeaters
from House h join MonthlyReport mr
on h.HouseID =mr.HouseID;




SELECT ht.HeaterID, ht.HeaterType, AVG(du.WorkHours) AS AverageDailyHours
FROM Heater ht JOIN DailyUsage du 
ON du.HeaterID = ht.HeaterID
GROUP BY ht.HeaterID, ht.HeaterType;



SELECT h.HouseID, h.Address, MAX(du.HeaterValue) AS MaxHeaterValue
FROM House h JOIN Heater ht 
ON ht.HouseID = h.HouseID
JOIN DailyUsage du 
ON du.HeaterID = ht.HeaterID
GROUP BY h.HouseID, h.Address;




SELECT h.HouseID, h.Address, COUNT(du.UsageID) AS TotalRecords
FROM House h JOIN Heater ht 
ON ht.HouseID = h.HouseID
JOIN DailyUsage du 
ON du.HeaterID = ht.HeaterID
GROUP BY h.HouseID, h.Address;


SELECT o.OwnerName, h.HouseID, h.Address, h.City
FROM Owner o JOIN House h 
ON h.OwnerID = o.OwnerID;


SELECT h.Address, ht.HeaterID, ht.HeaterType, ht.Power,ht.Efficiency
FROM House h JOIN Heater ht 
ON ht.HouseID = h.HouseID;



SELECT ht.HeaterType, ht.Power, du.UsageDate, du.WorkHours, du.HeaterValue
FROM Heater ht JOIN DailyUsage du
ON du.HeaterID = ht.HeaterID;



SELECT h.Address, mr.ReportMonth, mr.TotalWorkingHours, mr.MonthlyAverageCost
FROM House h JOIN MonthlyReport mr 
ON mr.HouseID = h.HouseID;















