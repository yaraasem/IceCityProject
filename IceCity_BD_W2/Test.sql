EXEC CalculateDailyHeaterUsageCost @HeaterID = 1, @UsageDate = '2026-04-01';
 

EXEC CalculateAndInsertMonthlyReport @HouseID = 1, @ReportMonth = '2026-04-01';
 

SELECT * FROM vw_HouseHeaterSummary;
SELECT * FROM vw_MonthlyCostSummary;
