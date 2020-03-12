/****** Object:  Table [dbo].[tbl_Category]    Script Date: 3/12/2020 5:09:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO tbl_Category VALUES ('Electronics')
GO
INSERT INTO tbl_Category VALUES ('Clothing')
GO
INSERT INTO tbl_Category VALUES ('Kitchen')
GO

/****** Object:  Table [dbo].[tbl_ContentLimitInsurance]    Script Date: 3/12/2020 5:11:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_ContentLimitInsurance](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [varchar](500) NULL,
	[ItemValue] [int] NULL,
	[CategoryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_ContentLimitInsurance]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[tbl_Category] ([CategoryID])
GO


/****** Object:  StoredProcedure [dbo].[usp_GetItems]    Script Date: 3/11/2020 9:43:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetItems]
AS
BEGIN
	DECLARE @Results TABLE(CategoryName VARCHAR(500), ItemID INT, ItemName VARCHAR(1000),ItemValue INT, GroupingLevel SMALLINT)

	----------------------------
	-- Insert the Saved Items --
	----------------------------
	INSERT INTO @Results
		SELECT C.CategoryName, 
				CLI.ItemID,
				CLI.ItemName,
				CLI.ItemValue,
				C.CategoryID * 10
		FROM tbl_Category C 
		INNER JOIN tbl_ContentLimitInsurance CLI ON C.CategoryID = CLI.CategoryID

	--------------------------------------
	-- Insert rows for Categories Total --
	--------------------------------------
	INSERT INTO @Results
		SELECT R.CategoryName,
				0,
				R.CategoryName,
				SUM(R.ItemValue),
				R.GroupingLevel / 10
	FROM @Results R 
	GROUP BY CategoryName, R.GroupingLevel

	----------------------------------------
	-- Insert row for  Total of all Items --
	----------------------------------------
	INSERT INTO @Results 
		SELECT 'Total',
				0,
				'Total',
				SUM(ItemValue),
				9999
		FROM @Results
		WHERE ItemID <> 0

	SELECT CategoryName, ItemName,ItemValue, GroupingLevel,ItemID FROM @Results ORDER BY CategoryName, GroupingLevel
END

