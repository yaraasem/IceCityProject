INSERT INTO Owner (OwnerName, Email, ContactNumber) VALUES
('Ahmed Hassan',   'ahmed@icecity.com',   '01001234567'),
('Sara Mohamed',   'sara@icecity.com',    '01112345678'),
('Omar Khalil',    'omar@icecity.com',    '01223456789');



INSERT INTO House (City, Address, OwnerID) VALUES
('IceCity', '12 Frost Street',     1),
('IceCity', '45 Blizzard Avenue',  1),
('IceCity', '7 Snow Lane',         2),
('IceCity', '99 Arctic Road',      2),
('IceCity', '3 Glacier Path',      3),
('IceCity', '21 Tundra Boulevard', 3);



INSERT INTO Heater (HeaterType, Power, Efficiency, IsActive, HouseID) VALUES
-- House 1
('Electric', 1500, NULL, 1, 1),
('Gas',      2000, 0.75, 1, 1),
-- House 2
('Solar',    1800, 0.70, 1, 2),
('Electric', 1200, NULL, 1, 2),
-- House 3
('Gas',      2200, 0.75, 1, 3),
('Electric', 1600, NULL, 1, 3),
-- House 4
('Solar',    1900, 0.70, 1, 4),
('Gas',      2100, 0.75, 1, 4),
-- House 5
('Electric', 1400, NULL, 1, 5),
('Solar',    2000, 0.70, 1, 5),
-- House 6
('Gas',      1800, 0.75, 1, 6),
('Electric', 1700, NULL, 1, 6);


DECLARE @HeaterID INT = 1;
DECLARE @Day INT;
DECLARE @NumberOfHeaters INT;

select @NumberOfHeaters=count(*)
from Heater;


 
WHILE @HeaterID <= @NumberOfHeaters
BEGIN
    SET @Day = 1;
    WHILE @Day <= 30
    BEGIN
        INSERT INTO DailyUsage (WorkHours, HeaterValue, UsageDate, HeaterID)
        VALUES
		(
            4 + (@Day % 10), --نغير عدد الساعات
          
            (SELECT Power FROM Heater WHERE HeaterID = @HeaterID) 
                * (0.7 + (@Day % 4) * 0.1),  --يجيب الباور ويضرب علشان نغير الفيم
           
            DATEADD
			(
			DAY, @Day - 1, --علشان @day=1 
			DATEADD
			(MONTH, -1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
			),
            @HeaterID
        );
        SET @Day = @Day + 1;
    END
    SET @HeaterID = @HeaterID + 1;
END



INSERT INTO MonthlyReport (ReportMonth, TotalWorkingHours, MonthlyAverageCost, HouseID)
SELECT
    DATEADD(MONTH, -1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)),
    SUM(du.WorkHours),
    AVG(du.HeaterValue) * (SUM(du.WorkHours) / 720.0),
    h.HouseID
FROM House h JOIN Heater ht 
ON ht.HouseID = h.HouseID
JOIN DailyUsage du ON du.HeaterID = ht.HeaterID
GROUP BY h.HouseID;


UPDATE MonthlyReport
SET MonthlyTotalCost =
(
    SELECT SUM(ht.Power * du.WorkHours * 0.05) --TotalCost = SUM(Power * WorkHours * CostRate)

    FROM Heater ht
    JOIN DailyUsage du ON du.HeaterID = ht.HeaterID
    WHERE ht.HouseID = MonthlyReport.HouseID
      AND MONTH(du.UsageDate) = MONTH(MonthlyReport.ReportMonth)
      AND YEAR(du.UsageDate)  = YEAR(MonthlyReport.ReportMonth)
);



UPDATE MonthlyReport
SET TotalWorkingHours = x.TotalHours
FROM MonthlyReport mr
JOIN (
    SELECT 
        h.HouseID,
        SUM(du.WorkHours) AS TotalHours
    FROM House h
    JOIN Heater ht ON ht.HouseID = h.HouseID
    JOIN DailyUsage du ON du.HeaterID = ht.HeaterID
    GROUP BY h.HouseID
) x
ON mr.HouseID = x.HouseID;
 
