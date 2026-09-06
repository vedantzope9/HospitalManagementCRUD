--CRUD - Hospital Management -- Hospital , Doctor, Appointment
use demo

IF SCHEMA_ID('demo') IS NULL
BEGIN
	EXEC('CREATE SCHEMA demo');
END

use demo

IF OBJECT_ID('demo.Hospital') IS NULL
BEGIN
	CREATE TABLE demo.HOSPITAL(
		HospitalId INT IDENTITY(1,1) PRIMARY KEY,
		HospitalName NVARCHAR(250) NOT NULL,
		City NVARCHAR(100),
		State NVARCHAR(100),
		PinCode INT,
		PhoneNumber NVARCHAR(15),
		Email NVARCHAR(100),
		IsActive BIT NOT NULL
	)
END;

IF OBJECT_ID('demo.Enum_Table') IS  NULL
BEGIN
	CREATE TABLE demo.Enum_Table(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		EnumGroup NVARCHAR(100) NOT NULL,
		EnumId INT NOT NULL,
		DisplayText NVARCHAR(100) NOT NULL,
		Value NVARCHAR(100) NOT NULL,
		LastModifiedDate DATETIME DEFAULT GETDATE(),
		IsActive BIT NOT NULL
	);
END

IF OBJECT_ID('demo.Doctor') IS  NULL
BEGIN
	CREATE TABLE demo.DOCTOR(
		DoctorId INT IDENTITY(1,1) PRIMARY KEY,
		UserId INT NOT NULL,
		DoctorName NVARCHAR(100) NOT NULL,
		HospitalId INT,
		Specialization INT NOT NULL,
		Qualification NVARCHAR(100),
		Experience INT,
		ConsultationFee DECIMAL(10,2) NOT NULL,
		IsAvailable BIT NOT NULL,
		IsActive BIT NOT NULL,
		CONSTRAINT FK_HospitalId_Doctor FOREIGN KEY (HospitalId) REFERENCES demo.Hospital(HospitalId),
		CONSTRAINT FK_UserId_Doctor FOREIGN KEY (Userid) REFERENCES demo.SecLoginUser(UserId),
		--CONSTRAINT FK_Specialization_Doctor FOREIGN KEY (Specialization) REFERENCES demo.Enum_Table(EnumId)
	);
END;

use demo;

IF OBJECT_ID('demo.SecLoginUser') IS  NULL
BEGIN
	CREATE TABLE demo.SecLoginUser(
		UserId INT IDENTITY(1,1) PRIMARY KEY,
		UserName NVARCHAR(250) NOT NULL,
		Password NVARCHAR(250) NOT NULL,
		Gender INT NOT NULL,
		PhoneNumber NVARCHAR(15) NOT NULL,
		Email NVARCHAR(100),
		Role INT NOT NULL,	--Doctor-1 , Patient-2
		IsActive BIT NOT NULL
	);
END


IF OBJECT_ID('demo.Appointment') IS  NULL
BEGIN
	CREATE TABLE demo.APPOINTMENT(
		AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
		DoctorId INT NOT NULL,
		UserId INT NOT NULL,
		PatientAge INT NOT NULL,
		AppointmentDate DATE,
		AppointmentTime TIME,
		DiseaseDescription NVARCHAR(200),
		Status INT NOT NULL,
		FeesPaid DECIMAL(10,2) NOT NULL,
		LastModifiedDate DATETIME DEFAULT GETDATE(),
		CONSTRAINT FK_DoctorId_Appointment FOREIGN KEY (DoctorId) REFERENCES demo.Doctor(DoctorId),
		CONSTRAINT FK_UserId_Appointment FOREIGN KEY (UserId) REFERENCES demo.SecLoginUser(UserId),
		--CONSTRAINT FK_Gender_Appointment FOREIGN KEY (Gender) REFERENCES demo.Enum_Table(EnumId),
		--CONSTRAINT FK_Status_Appointment FOREIGN KEY (Status) REFERENCES demo.Enum_Table(EnumId),
	);
END


ALTER TABLE demo.Enum_Table ADD LockId INT NOT NULL DEFAULT 0

ALTER TABLE demo.APPOINTMENT ADD LockId INT NOT NULL DEFAULT 0

ALTER TABLE demo.SecLoginUser ADD LockId INT NOT NULL DEFAULT 0

ALTER TABLE demo.DOCTOR  ADD LockId INT NOT NULL DEFAULT 0

ALTER TABLE demo.HOSPITAL ADD LockId INT NOT NULL DEFAULT 0

/*
--Bootstrap data

INSERT INTO demo.Enum_Table VALUES 
	('Gender' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Gender') , 'Male','MALE', GETDATE(), 1),
	('Gender' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Gender') , 'Female','FEMALE', GETDATE(), 'true');

--NOTE: Run each Insert query one by one --EnumId 
INSERT INTO demo.Enum_Table VALUES 
	('Status' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Status') , 'Booked','BOOKED', GETDATE(), 'true'),
	('Status' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Status') , 'Completed','COMPLETED', GETDATE(), 'true'),
	('Status' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Status') , 'Confirmed','CONFIRMED', GETDATE(), 'true'),
	('Status' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Status') , 'Cancelled','CANCELLED', GETDATE(), 'true'),
	('Status' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Status') , 'Rescheduled','RESCHEDULED', GETDATE(), 'true'),
	('Status' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Status') , 'No Show','NO_SHOW', GETDATE(), 'true');


INSERT INTO demo.Enum_Table(EnumGroup,EnumId,DisplayText,Value,IsActive)
VALUES
('Specialization',1,'General Physician','GENERAL_PHYSICIAN',1),
('Specialization',2,'Cardiologist','CARDIOLOGIST',1),
('Specialization',3,'Dermatologist','DERMATOLOGIST',1),
('Specialization',4,'Neurologist','NEUROLOGIST',1),
('Specialization',5,'Orthopedic Surgeon','ORTHOPEDIC',1),
('Specialization',6,'Pediatrician','PEDIATRICIAN',1),
('Specialization',7,'Gynecologist','GYNECOLOGIST',1),
('Specialization',8,'Psychiatrist','PSYCHIATRIST',1),
('Specialization',9,'ENT Specialist','ENT',1),
('Specialization',10,'Ophthalmologist','OPHTHALMOLOGIST',1),
('Specialization',11,'Dentist','DENTIST',1),
('Specialization',12,'Urologist','UROLOGIST',1),
('Specialization',13,'Nephrologist','NEPHROLOGIST',1),
('Specialization',14,'Gastroenterologist','GASTROENTEROLOGIST',1),
('Specialization',15,'Pulmonologist','PULMONOLOGIST',1),
('Specialization',16,'Oncologist','ONCOLOGIST',1),
('Specialization',17,'Endocrinologist','ENDOCRINOLOGIST',1),
('Specialization',18,'Radiologist','RADIOLOGIST',1),
('Specialization',19,'Anesthesiologist','ANESTHESIOLOGIST',1),
('Specialization',20,'Emergency Medicine','EMERGENCY_MEDICINE',1);

INSERT INTO demo.Enum_Table VALUES 
	('Role' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Role') , 'Admin','ADMIN', GETDATE(), 1)
INSERT INTO demo.Enum_Table VALUES 
	('Role' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Role') , 'Doctor','DOCTOR', GETDATE(), 1)
INSERT INTO demo.Enum_Table VALUES 
	('Role' , (Select ISNULL( Max(EnumId) , 0)+1 From demo.Enum_Table WHERE EnumGroup = 'Role') , 'User','USER', GETDATE(), 1)

INSERT INTO demo.SecLoginUser 
VALUES('admin' , 'admin@123' , (Select EnumId From demo.Enum_Table where Value = 'Male') , '0101010101' , 'admin@domain.com' , (Select EnumId From demo.Enum_Table where Value = 'ADMIN') , 1)

*/