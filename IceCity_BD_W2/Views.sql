CREATE VIEW vw_HouseHeaterSummary AS
SELECT
    h.HouseID        AS HouseID,
    h.Address        AS HouseAddress,
    ht.HeaterID      AS HeaterID,
    ht.HeaterType    AS HeaterType,
    ht.Power         AS PowerValue,
    ht.IsActive      AS IsActive
FROM House h JOIN Heater ht  
ON ht.HouseID = h.HouseID;

 go




CREATE VIEW vw_MonthlyCostSummary AS
SELECT
    h.HouseID                  AS HouseID,
    h.Address                  AS HouseAddress,
    mr.ReportMonth             AS ReportMonth,
    mr.TotalWorkingHours       AS TotalWorkingHours,
    mr.MonthlyAverageCost      AS MonthlyAverageCost
FROM House h JOIN MonthlyReport mr
ON mr.HouseID = h.HouseID;
