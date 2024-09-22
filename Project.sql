IF db_id('College1en') IS NULL 
    CREATE DATABASE College1en;
GO

USE College1en;

BEGIN TRY
    DROP TABLE Enrollments;
END TRY
BEGIN CATCH
END CATCH;

BEGIN TRY
    DROP TABLE Students;
END TRY
BEGIN CATCH
END CATCH;

BEGIN TRY
    DROP TABLE Courses;
END TRY
BEGIN CATCH
END CATCH;

BEGIN TRY
    DROP TABLE Programs;
END TRY
BEGIN CATCH
END CATCH;

CREATE TABLE Programs (
    ProgId VARCHAR(5) NOT NULL, 
    ProgName VARCHAR(50) NOT NULL,
    PRIMARY KEY (ProgId)
);

CREATE TABLE Courses (
    CId VARCHAR(7) NOT NULL, 
    CName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    PRIMARY KEY (CId),
    FOREIGN KEY (ProgId) 
	REFERENCES Programs(ProgId)
	    ON DELETE CASCADE
        ON UPDATE CASCADE
	);
	GO

CREATE TABLE Students (
    StId VARCHAR(10) NOT NULL, 
    StName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    PRIMARY KEY (StId),
    FOREIGN KEY (ProgId) 
	REFERENCES Programs(ProgId)
		ON DELETE CASCADE
        ON UPDATE CASCADE
	);
	GO



CREATE TABLE Enrollments (
    StId VARCHAR(10) NOT NULL, 
    CId VARCHAR(7) NOT NULL,
    FinalGrade INT,
    PRIMARY KEY (StId, CId),
    FOREIGN KEY (StId) 
	REFERENCES Students(StId)
		ON DELETE CASCADE
        ON UPDATE CASCADE,
    FOREIGN KEY (CId) 
	REFERENCES Courses(CId)
		ON DELETE NO ACTION
        ON UPDATE NO ACTION
	);
	GO

INSERT INTO Programs (ProgId, ProgName) VALUES
('P0001', 'Industrial Engineering'),
('P0002', 'Psychologie'),
('P0003', 'Computer Science');

INSERT INTO Courses (CId, CName, ProgId) VALUES
('C000001', 'Course 1', 'P0001'),
('C000002', 'Course 2', 'P0001'),
('C000003', 'Course 3', 'P0002'),
('C000004', 'Course 4', 'P0002'),
('C000005', 'Course 5', 'P0003');

INSERT INTO Students (StId, StName, ProgId) VALUES
('S000000001', 'Student 1', 'P0001'),
('S000000002', 'Student 2', 'P0001'),
('S000000003', 'Student 3', 'P0002'),
('S000000004', 'Student 4', 'P0002'),
('S000000005', 'Student 5', 'P0003');

SELECT * FROM Programs;
SELECT * FROM Courses;
SELECT * FROM Students;
SELECT * FROM Enrollments;