CREATE PROCEDURE CalculateDailyHeaterUsageCost
    @HeaterID  INT,
    @UsageDate DATE
AS
BEGIN
    
    DECLARE @Power       FLOAT;
    DECLARE @WorkHours   FLOAT;
    DECLARE @DailyCost   FLOAT;
 
   
    SELECT @Power = Power
    FROM Heater
    WHERE HeaterID = @HeaterID;
 
   
   SELECT @WorkHours = SUM(WorkHours) 
   FROM DailyUsage
   WHERE HeaterID = @HeaterID
  AND UsageDate = @UsageDate;
 
   
    -- DailyCost = Power * WorkHours * CostRate (0.05 لكل كيلوواط/ساعة)
    SET @DailyCost = @Power * @WorkHours * 0.05;
 
    
    SELECT
        @WorkHours AS WorkingHours,
        @Power     AS HeaterPower,
        @DailyCost AS DailyCost;
END
go



CREATE PROCEDURE CalculateAndInsertMonthlyReport
    @HouseID     INT,
    @ReportMonth DATE
AS
BEGIN
    DECLARE @TotalHours FLOAT;
    DECLARE @AvgValue   FLOAT;
    DECLARE @MonthlyCost FLOAT;
 
    
    SELECT
        @TotalHours = SUM(du.WorkHours),
        @AvgValue   = AVG(du.HeaterValue)
    FROM DailyUsage du JOIN Heater ht  
	ON ht.HeaterID = du.HeaterID
    WHERE ht.HouseID  = @HouseID
      AND MONTH(du.UsageDate) = MONTH(@ReportMonth)
      AND YEAR(du.UsageDate)  = YEAR(@ReportMonth);
 
   
    SET @MonthlyCost = @AvgValue * (@TotalHours / 720.0);
 
    
    INSERT INTO MonthlyReport (ReportMonth, TotalWorkingHours, MonthlyAverageCost, HouseID)
    VALUES (@ReportMonth, @TotalHours, @MonthlyCost, @HouseID);
 
    
    SELECT
        @HouseID     AS HouseID,
        @ReportMonth AS ReportMonth,
        @TotalHours  AS TotalWorkingHours,
        @MonthlyCost AS MonthlyAverageCost;
END
