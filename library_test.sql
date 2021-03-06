CREATE DATABASE library_test
GO
USE [library_test]
GO
/****** Object:  Table [dbo].[authors]    Script Date: 7/20/2016 4:39:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[authors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[books]    Script Date: 7/20/2016 4:39:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[books_authors]    Script Date: 7/20/2016 4:39:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books_authors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[author_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[checkouts]    Script Date: 7/20/2016 4:39:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[checkouts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[copy_id] [int] NULL,
	[patron_id] [int] NULL,
	[checkout_date] [datetime] NULL,
	[due_date] [datetime] NULL,
	[returned] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[copies]    Script Date: 7/20/2016 4:39:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[copies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[status] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[patrons]    Script Date: 7/20/2016 4:39:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[patrons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[phonenum] [varchar](15) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[books_authors] ON 

INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (1, 20, 8)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (2, 21, 11)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (3, 23, 13)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (4, 25, 15)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (5, 27, 17)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (6, 29, 19)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (7, 31, 21)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (8, 34, 22)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (9, 36, 24)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (10, 37, 27)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (14, 44, 32)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (12, 39, 29)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (15, 46, 34)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (20, 52, 40)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (19, 51, 39)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (21, 54, 42)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (24, 57, 47)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (32, 68, 56)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (26, 59, 49)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (31, 67, 55)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (33, 70, 58)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (38, 76, 65)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (37, 75, 64)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (39, 79, 67)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (44, 85, 74)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (43, 84, 73)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (45, 88, 76)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (50, 94, 83)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (47, 90, 79)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (49, 93, 82)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (54, 100, 91)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (62, 112, 101)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (56, 102, 94)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (61, 111, 100)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (68, 121, 110)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (67, 120, 109)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (74, 130, 119)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (73, 129, 118)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (75, 133, 121)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (78, 136, 127)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (84, 145, 136)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (80, 138, 130)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (92, 157, 146)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (86, 147, 139)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (91, 156, 145)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (98, 166, 155)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (97, 165, 154)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (104, 175, 164)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (103, 174, 163)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (108, 181, 172)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (116, 193, 182)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (110, 183, 175)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (115, 192, 181)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (120, 199, 190)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (128, 211, 200)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (122, 201, 193)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (127, 210, 199)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (134, 220, 209)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (133, 219, 208)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (140, 229, 218)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (139, 228, 217)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (146, 238, 227)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (145, 237, 226)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (152, 247, 236)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (151, 246, 235)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (156, 253, 244)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (164, 265, 254)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (158, 255, 247)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (163, 264, 253)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (170, 274, 263)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (169, 273, 262)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (176, 283, 272)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (175, 282, 271)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (182, 292, 281)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (181, 291, 280)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (188, 301, 290)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (185, 297, 286)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (187, 300, 289)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (201, 322, 310)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (191, 306, 295)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (194, 309, 301)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (196, 312, 304)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (206, 328, 317)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (203, 324, 313)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (205, 327, 316)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (207, 331, 319)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (215, 340, 332)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (209, 333, 322)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (212, 336, 328)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (214, 339, 331)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (13, 41, 31)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (53, 99, 88)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (65, 117, 106)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (71, 126, 115)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (95, 162, 151)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (101, 171, 160)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (107, 180, 169)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (119, 198, 187)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (131, 216, 205)
GO
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (137, 225, 214)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (143, 234, 223)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (149, 243, 232)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (155, 252, 241)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (167, 270, 259)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (173, 279, 268)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (179, 288, 277)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (17, 48, 36)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (125, 205, 197)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (161, 259, 251)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (183, 295, 283)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (23, 56, 44)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (35, 72, 61)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (189, 304, 292)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (29, 63, 53)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (28, 62, 52)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (59, 106, 98)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (83, 142, 134)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (89, 151, 143)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (113, 187, 179)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (192, 307, 298)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (124, 204, 196)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (160, 258, 250)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (210, 334, 325)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (51, 97, 85)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (41, 81, 70)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (63, 115, 103)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (69, 124, 112)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (93, 160, 148)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (77, 135, 124)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (99, 169, 157)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (105, 178, 166)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (117, 196, 184)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (129, 214, 202)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (135, 223, 211)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (141, 232, 220)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (147, 241, 229)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (153, 250, 238)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (165, 268, 256)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (171, 277, 265)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (177, 286, 274)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (200, 319, 308)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (58, 105, 97)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (82, 141, 133)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (88, 150, 142)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (112, 186, 178)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (197, 313, 305)
INSERT [dbo].[books_authors] ([id], [book_id], [author_id]) VALUES (199, 318, 307)
SET IDENTITY_INSERT [dbo].[books_authors] OFF
