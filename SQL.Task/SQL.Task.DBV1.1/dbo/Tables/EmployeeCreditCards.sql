CREATE TABLE "EmployeeCreditCards"(
"CardNumber" "int" NOT NULL,
"EndDate" "datetime" NULL,
"CardHolderName" nvarchar(25) NULL,
"EmployeeID" "int" NOT NULL,
CONSTRAINT "PK_CardNumber" PRIMARY KEY  CLUSTERED 
(
	"CardNumber"
),
CONSTRAINT "FK_EmployeeCreditCards_Employees" FOREIGN KEY(
"EmployeeID"
) REFERENCES "dbo"."Employees"
(
"EmployeeID"
)
)
