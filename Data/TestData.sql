USE [scheduler]
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

GO
INSERT [dbo].[Groups] ([Id], [Name], [Quantity], [YearOfReceipt], [Faculty_Id]) VALUES (1, N'1', 30, 2014, 1)

SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[LessonTimes] ON 

GO
INSERT [dbo].[LessonTimes] ([Id], [TimeOfBegin], [TimeOfEnd]) VALUES (1, CAST(N'2015-05-24 09:50:00.000' AS DateTime), CAST(N'2015-05-24 11:25:00.000' AS DateTime))
GO
INSERT [dbo].[LessonTimes] ([Id], [TimeOfBegin], [TimeOfEnd]) VALUES (2, CAST(N'2015-05-24 11:40:00.000' AS DateTime), CAST(N'2015-05-24 13:15:00.000' AS DateTime))
GO
INSERT [dbo].[LessonTimes] ([Id], [TimeOfBegin], [TimeOfEnd]) VALUES (3, CAST(N'2015-05-24 13:30:00.000' AS DateTime), CAST(N'2015-05-24 15:05:00.000' AS DateTime))
GO
INSERT [dbo].[LessonTimes] ([Id], [TimeOfBegin], [TimeOfEnd]) VALUES (4, CAST(N'2015-05-24 15:20:00.000' AS DateTime), CAST(N'2015-05-24 16:55:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[LessonTimes] OFF
GO
SET IDENTITY_INSERT [dbo].[Semesters] ON 

GO
INSERT [dbo].[Semesters] ([Id], [Name], [BeginOfSemester], [EndOfSemester]) VALUES (1, N'1ый семестр 2015 года', CAST(N'2015-09-01 00:00:00.000' AS DateTime), CAST(N'2015-12-29 00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Semesters] OFF
GO
SET IDENTITY_INSERT [dbo].[ScheduleItems] ON 

GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (1, 2015, 1, 1, 1, 5, 1, 5, 10, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (2, 2015, 1, 1, 2, 6, 2, 6, 12, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (3, 2015, 1, 1, 1, 4, 3, 15, 9, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (4, 2015, 1, 2, 3, 10, 2, 55, 15, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (5, 2015, 1, 2, 1, 9, 3, 90, 50, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (6, 2015, 1, 2, 2, 50, 4, 5, 1, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (7, 2015, 1, 3, 1, 5, 1, 6, 12, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (8, 2015, 1, 3, 2, 9, 2, 66, 10, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (9, 2015, 1, 4, 3, 22, 3, 222, 12, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (10, 2015, 1, 5, 1, 6, 1, 90, 4, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (11, 2015, 1, 5, 2, 6, 2, 90, 4, 1, 1)
GO
INSERT [dbo].[ScheduleItems] ([Id], [Year], [WeekNumber], [DayOfWeekItemId], [LessonTypeId], [AuditoriumId], [LessonTimeId], [TeacherId], [SubjectId], [GroupId], [SemesterId]) VALUES (12, 2015, 1, 6, 3, 10, 4, 21, 9, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ScheduleItems] OFF
GO
