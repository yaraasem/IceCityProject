create database IceCity;
use IceCity;

create table [Owner]
(
OwnerID        INT  PRIMARY KEY IDENTITY(1,1),
OwnerName      VARCHAR(50)  NOT NULL,
Email          VARCHAR(50)  NOT NULL,
ContactNumber  VARCHAR(15)   NOT NULL
);



CREATE TABLE House
(
HouseID   INT		    PRIMARY KEY IDENTITY(1,1),
OwnerID   INT           NOT NULL,
City      VARCHAR(100)  NOT NULL,
Address   VARCHAR(200)  NOT NULL,
FOREIGN KEY (OwnerID) REFERENCES Owner(OwnerID)
);



cREATE TABLE Heater 
(
HeaterID    INT           PRIMARY KEY IDENTITY(1,1),
HouseID     INT           NOT NULL,
HeaterType  VARCHAR(20)   NOT NULL,  
Power      FLOAT         NOT NULL CHECK (Power >= 0),
Efficiency  FLOAT,       -- NULL for Electric, 0.75 Gas, 0.70 Solar
IsActive    BIT           NOT NULL DEFAULT 1,
FOREIGN KEY (HouseID) REFERENCES House(HouseID)
);


CREATE TABLE DailyUsage
(
UsageID      INT      PRIMARY KEY IDENTITY(1,1),
HeaterID     INT      NOT NULL,
WorkHours    FLOAT    NOT NULL CHECK (WorkHours >= 0 AND WorkHours <= 24),
HeaterValue  FLOAT    NOT NULL CHECK (HeaterValue >= 0),
UsageDate    DATE     NOT NULL,
FOREIGN KEY (HeaterID) REFERENCES Heater(HeaterID)
);



CREATE TABLE MonthlyReport
(
ReportID            INT      PRIMARY KEY IDENTITY(1,1),
HouseID             INT      NOT NULL,
ReportMonth         DATE     NOT NULL,
TotalWorkingHours   FLOAT    NOT NULL,
MonthlyAverageCost  FLOAT    NOT NULL,
FOREIGN KEY (HouseID) REFERENCES House(HouseID)
);

alter table MonthlyReport
add MonthlyTotalCost float ;



ALTER TABLE MonthlyReport
ADD HeaterID INT FOREIGN KEY REFERENCES Heater(HeaterID);



SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('MonthlyReport');
ALTER TABLE MonthlyReport
DROP CONSTRAINT FK__MonthlyRe__Heate__5AEE82B9;
alter table MonthlyReport
drop column HeaterID;