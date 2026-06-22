USE [master]
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'FUNewsManagement')
BEGIN
    ALTER DATABASE [FUNewsManagement] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [FUNewsManagement];
END
GO

CREATE DATABASE [FUNewsManagement]
GO

USE [FUNewsManagement]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category](
	[CategoryID] [smallint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[CategoryDesciption] [nvarchar](250) NOT NULL,
	[ParentCategoryID] [smallint] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewsArticle](
	[NewsArticleID] [nvarchar](20) NOT NULL,
	[NewsTitle] [nvarchar](400) NULL,
	[Headline] [nvarchar](150) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[NewsContent] [nvarchar](4000) NULL,
	[NewsSource] [nvarchar](400) NULL,
	[CategoryID] [smallint] NULL,
	[NewsStatus] [bit] NULL,
	[CreatedByID] [smallint] NULL,
	[UpdatedByID] [smallint] NULL,
	[ModifiedDate] [datetime] NULL,
	[ImageUrl] [nvarchar](500) NULL,
	[ViewCount] [int] NULL,
 CONSTRAINT [PK_NewsArticle] PRIMARY KEY CLUSTERED 
(
	[NewsArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewsTag](
	[NewsArticleID] [nvarchar](20) NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_NewsTag] PRIMARY KEY CLUSTERED 
(
	[NewsArticleID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemAccount](
	[AccountID] [smallint] NOT NULL,
	[AccountName] [nvarchar](100) NULL,
	[AccountEmail] [nvarchar](70) NULL,
	[AccountRole] [int] NULL,
	[AccountPassword] [nvarchar](70) NULL,
 CONSTRAINT [PK_SystemAccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tag](
	[TagID] [int] NOT NULL,
	[TagName] [nvarchar](50) NULL,
	[Note] [nvarchar](400) NULL,
 CONSTRAINT [PK_HashTag] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO


INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDesciption], [ParentCategoryID], [IsActive]) VALUES (1, N'Academic news', N'This category can include articles about research findings, faculty appointments and promotions, and other academic-related announcements.', 1, 1)
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDesciption], [ParentCategoryID], [IsActive]) VALUES (2, N'Student Affairs', N'This category can include articles about student activities, events, and initiatives, such as student clubs, organizations and sports.', 2, 1)
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDesciption], [ParentCategoryID], [IsActive]) VALUES (3, N'Campus Safety', N'This category can include articles about incidents and safety measures implemented on campus to ensure the safety of students and faculty.', 3, 1)
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDesciption], [ParentCategoryID], [IsActive]) VALUES (4, N'Alumni News', N'This category can include articles about the achievements and accomplishments of former students and alumni, such as graduations, job promotions and career successes.', 4, 1)
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDesciption], [ParentCategoryID], [IsActive]) VALUES (5, N'Capstone Project News', N'This category is typically a comprehensive and detailed report created as part of an academic or professional capstone project. ', 5, 0)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO


INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate]) VALUES (N'1', N'University FU Celebrates Success of Alumni in Various Fields', N'University FU Celebrates Success of Alumni in Various Fields', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'University FU recently commemorated the achievements of its esteemed alumni who have excelled in a multitude of fields, showcasing the impact of the institution''s education on their professional journeys.

Diverse Success Stories: From successful entrepreneurs to renowned artists, University X''s alumni have made significant strides in various industries, reflecting the versatility of the education provided.

Networking Opportunities: The university''s strong alumni network played a crucial role in fostering connections and collaborations among graduates, contributing to their success.

Alumni Contributions: Many alumni have also given back to the university through mentorship programs, scholarships, and guest lectures, enriching the current student experience.', N'N/A', 4, 1, 1, 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate]) VALUES (N'2', N'Alumni Association Launches Mentorship Program for Recent Graduates', N'Alumni Association Launches Mentorship Program for Recent Graduates', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'The Alumni Association of University FU recently unveiled a new mentorship program aimed at providing support and guidance to recent graduates as they navigate the transition from academia to the professional world.

The program pairs recent graduates with experienced alumni mentors based on their career interests and goals, ensuring tailored guidance for each mentee.

In addition to one-on-one mentorship, the program offers workshops on resume building, interview preparation, and networking strategies to equip graduates with essential skills for success.

The mentorship program also facilitates networking events where mentees can expand their professional connections and learn from alumni who have excelled in their respective fields.

By fostering a supportive network of alumni mentors, the program aims to empower recent graduates to navigate the challenges of the job market, enhance their professional growth, and build lasting connections within their industries.

The launch of this mentorship program by the Alumni Association of University Y underscores the commitment to nurturing the success of its graduates beyond graduation, creating a strong community of support and guidance for the next generation of professionals.', N'Internet', 4, 1, 1, 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate]) VALUES (N'3', N'Academic Department Announces Groundbreaking Initiatives and Program Enhancements', N'Academic Department Announces Groundbreaking Initiatives and Program Enhancements', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'The Software Engineering Department at FU has unveiled a series of transformative initiatives and program enhancements aimed at enriching the academic experience and fostering innovation in Software Development.

The department has established [specific research centers or facilities] dedicated to advancing knowledge and addressing pressing challenges in Software Development.

Faculty Promotions: Several esteemed faculty members have been promoted to key leadership positions, reflecting their commitment to academic excellence and their vision for shaping the future of Software Development.

The academic programs within the department have undergone enhancements to incorporate the latest developments and equip students with practical skills and knowledge relevant to current industry demands.

These initiatives are poised to position the Software Engineering Department as a hub of innovation and academic rigor, attracting top talent and fostering groundbreaking research and learning experiences.
', N'N/A', 1, 1, 2, 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate]) VALUES (N'4', N'Renowned Scholar Appointed as Head of AI Department at FU', N'Renowned Scholar Appointed as Head of AI Department at FU', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'FU proudly announces the appointment of David Nitzevet, a distinguished scholar in Machine Learning, to the prestigious position of Head of AI Department, underscoring the institution''s commitment to academic excellence and leadership.

David Nitzevet brings a wealth of experience and expertise to the role, with a notable track record of the development of deep learning algorithms and the application of machine learning in healthcare, finance, and marketing. In accepting the appointment, David Nitzevet expressed a vision to develop new algorithmic models, improve data preprocessing techniques, and enhance the interpretability of machine learning models, align with the university''s mission to drive innovation and excellence in Machine Learning.

The appointment is expected to foster collaborations and initiatives that will enrich the academic and research landscape of the university and beyond.

The addition of David Nitzevet to the AI Department faculty elevates the institution''s academic standing and promises to inspire students, scholars, and professionals in Machine Learning. The appointment reaffirms the university''s dedication to recruiting top-tier talent and nurturing an environment where academic distinction thrives.
', N'N/A', 1, 1, 2, 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate]) VALUES (N'5', N'New Research Findings Shed Light on STEM', N'New Research Findings Shed Light on STEM', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'Groundbreaking research conducted by the Research Department of FU has unveiled significant findings in the field of STEM, offering fresh insights that could revolutionize current understanding and practices.

The success of this research is attributed to the collaborative efforts of a multidisciplinary team, showcasing the institution''s commitment to fostering cutting-edge research. The newly uncovered knowledge has the potential to influence Math, Engineering, Technology and shape future research endeavors, positioning the Research Department of FU as a leader in the advancement of STEM.

The research findings stand as a testament to the institution''s dedication to impactful research and its contribution to the global knowledge base in STEM.', N'N/A', 1, 1, 2, 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime))
GO


INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'1', 5)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'1', 7)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'1', 9)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'2', 5)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'2', 7)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'2', 8)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'2', 9)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'3', 1)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'3', 8)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'3', 9)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'4', 1)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'4', 4)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'4', 8)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'4', 9)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'5', 2)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'5', 3)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'5', 4)
GO
INSERT [dbo].[NewsTag] ([NewsArticleID], [TagID]) VALUES (N'5', 6)
GO
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (1, N'Emma William', N'EmmaWilliam@FUNewsManagement.org', 2, N'@1')
GO
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (2, N'Olivia James', N'OliviaJames@FUNewsManagement.org', 2, N'@1')
GO
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (3, N'Isabella David', N'IsabellaDavid@FUNewsManagement.org', 1, N'@1')
GO
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (4, N'Michael Charlotte', N'MichaelCharlotte@FUNewsManagement.org', 1, N'@1')
GO
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (5, N'Steve Paris', N'SteveParis@FUNewsManagement.org', 1, N'@1')
GO

INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (1, N'Education', N'Education Note')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (2, N'Technology', N'Technology Note')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (3, N'Research', N'Research Note')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (4, N'Innovation', N'Innovation Note')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (5, N'Campus Life', N'Campus Life Note')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (6, N'Faculty', N'Faculty Achievements')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (7, N'Alumni ', N'Alumni News')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (8, N'Events', N'University Events')
GO
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (9, N'Resources', N'Campus Resources')
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([ParentCategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
ALTER TABLE [dbo].[NewsArticle]  WITH CHECK ADD  CONSTRAINT [FK_NewsArticle_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsArticle] CHECK CONSTRAINT [FK_NewsArticle_Category]
GO
ALTER TABLE [dbo].[NewsArticle]  WITH CHECK ADD  CONSTRAINT [FK_NewsArticle_SystemAccount] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[SystemAccount] ([AccountID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsArticle] CHECK CONSTRAINT [FK_NewsArticle_SystemAccount]
GO
ALTER TABLE [dbo].[NewsTag]  WITH CHECK ADD  CONSTRAINT [FK_NewsTag_NewsArticle] FOREIGN KEY([NewsArticleID])
REFERENCES [dbo].[NewsArticle] ([NewsArticleID])
GO
ALTER TABLE [dbo].[NewsTag] CHECK CONSTRAINT [FK_NewsTag_NewsArticle]
GO
ALTER TABLE [dbo].[NewsTag]  WITH CHECK ADD  CONSTRAINT [FK_NewsTag_Tag] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tag] ([TagID])
GO
ALTER TABLE [dbo].[NewsTag] CHECK CONSTRAINT [FK_NewsTag_Tag]
GO

-- ======= UPDATES & NEW DATA =======
UPDATE [dbo].[NewsArticle] 
SET ImageUrl = N'https://images.pexels.com/photos/267885/pexels-photo-267885.jpeg?auto=compress&cs=tinysrgb&w=1200',
ViewCount = 650
WHERE NewsArticleID = N'1';

UPDATE [dbo].[NewsArticle] 
SET ImageUrl = N'https://images.pexels.com/photos/3184398/pexels-photo-3184398.jpeg?auto=compress&cs=tinysrgb&w=1200',
ViewCount = 420
WHERE NewsArticleID = N'2';

UPDATE [dbo].[NewsArticle] 
SET ImageUrl = N'https://images.pexels.com/photos/1181675/pexels-photo-1181675.jpeg?auto=compress&cs=tinysrgb&w=1200',
ViewCount = 890
WHERE NewsArticleID = N'3';

UPDATE [dbo].[NewsArticle] 
SET ImageUrl = N'https://images.pexels.com/photos/3184291/pexels-photo-3184291.jpeg?auto=compress&cs=tinysrgb&w=1200',
ViewCount = 550
WHERE NewsArticleID = N'4';

UPDATE [dbo].[NewsArticle] 
SET ImageUrl = N'https://images.pexels.com/photos/159358/construction-site-build-construction-work-159358.jpeg?auto=compress&cs=tinysrgb&w=1200',
ViewCount = 780
WHERE NewsArticleID = N'5';
GO

INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreatedDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdatedByID], [ModifiedDate], [ImageUrl], [ViewCount]) VALUES 
(N'6', N'Women''s Basketball Semifinals Preview And Schedule', N'The highly anticipated semifinals are here.', CAST(N'2024-08-01T00:00:00.000' AS DateTime), N'The inaugural Olympic breaking competition kicked off in La Concorde on Friday. The women''s basketball semifinals are set to be a thrilling match-up between the top teams in the world.

The atmosphere in the arena was electric as fans from across the globe gathered to witness history. The teams have been preparing relentlessly for this moment, showcasing their dedication through grueling training sessions and strategic masterclasses. Experts predict an intensely competitive series that could redefine the landscape of women''s basketball for years to come.

Key players to watch include several rising stars who have dominated the group stages with their incredible agility and scoring prowess. Coaches have emphasized the importance of maintaining focus and executing defensive strategies flawlessly, as even the smallest mistake could cost them a spot in the finals.

As the tournament progresses, the excitement continues to build. Viewers tuning in from home can expect high-octane action, dramatic buzzer-beaters, and moments of sportsmanship that highlight the true spirit of the Olympic games.', N'Olympics', 3, 1, 1, 1, CAST(N'2024-08-01T00:00:00.000' AS DateTime), N'https://images.pexels.com/photos/1332237/pexels-photo-1332237.jpeg?auto=compress&cs=tinysrgb&w=1200', 350),

(N'7', N'Carlos Yulo wins historic gold for Philippines', N'A monumental achievement in gymnastics.', CAST(N'2024-08-02T00:00:00.000' AS DateTime), N'Carlos Yulo has made history by winning the gold medal in the men''s floor exercise. This marks the first-ever Olympic gymnastics medal for the Philippines.

His flawless routine captivated both the judges and the audience, earning him the highest score of the evening. The meticulous execution of his signature tumbling passes demonstrated years of unwavering dedication and sacrifice. Yulo''s victory has sparked nationwide celebrations in his home country, serving as an inspiration to millions of young aspiring athletes.

Following his win, Yulo expressed his immense gratitude to his coaches, family, and supporters who believed in his dream. He emphasized that this medal is not just his, but belongs to every Filipino who supported him throughout his journey. The government has already announced plans to host a grand hero''s welcome upon his return.

This historic milestone is expected to significantly boost the popularity and funding of gymnastics programs in the Philippines, paving the way for future generations to compete on the world stage.', N'Olympics', 4, 1, 1, 1, CAST(N'2024-08-02T00:00:00.000' AS DateTime), N'https://images.pexels.com/photos/163452/basketball-dunk-blue-game-163452.jpeg?auto=compress&cs=tinysrgb&w=1200', 420),

(N'8', N'Uzbekistan''s Rashitov defends Olympic men''s -68kg taekwondo title', N'A stunning display of martial arts mastery.', CAST(N'2024-08-03T00:00:00.000' AS DateTime), N'Ulugbek Rashitov of Uzbekistan successfully defended his Olympic title in the men''s -68kg taekwondo event. He defeated his opponents with precise strikes and unmatched agility.

Rashitov''s performance throughout the tournament was nothing short of spectacular. His ability to read his opponents'' movements and counter with lightning-fast kicks left spectators in awe. The final match was a tactical masterclass, with Rashitov maintaining absolute control over the pacing and distance from the very first round.

His victory firmly establishes him as one of the greatest taekwondo practitioners of his generation. Sports analysts have praised his discipline, mental fortitude, and the innovative techniques he brings to the mat. Back in Uzbekistan, his back-to-back Olympic gold medals have cemented his status as a national sports icon.

Looking ahead, Rashitov has expressed his desire to continue competing and eventually transition into coaching to pass down his knowledge. His achievements continue to inspire young martial artists worldwide to pursue excellence with passion and perseverance.', N'Olympics', 5, 1, 2, 2, CAST(N'2024-08-03T00:00:00.000' AS DateTime), N'https://images.pexels.com/photos/7045748/pexels-photo-7045748.jpeg?auto=compress&cs=tinysrgb&w=1200', 120),

(N'9', N'The People''s Republic of China reigns supreme in men''s team', N'Dominance in table tennis continues.', CAST(N'2024-08-04T00:00:00.000' AS DateTime), N'The men''s table tennis team from the People''s Republic of China has once again proven their dominance. They swept through the competition to claim the gold medal without dropping a single match.

The squad, composed of the world''s top-ranked players, showcased an unparalleled level of skill, speed, and precision. Their exceptional coordination in doubles matches and ruthless efficiency in singles play demonstrated why they remain the undisputed kings of the sport. The final against their long-time rivals was a breathtaking display of high-speed rallies and tactical brilliance.

Team captain Ma Long praised his teammates for their relentless work ethic and focus during the grueling training camps leading up to the games. He noted that the pressure to maintain China''s golden legacy is immense, but the team''s unity and mutual support were the keys to overcoming every challenge they faced.

This victory adds yet another glorious chapter to China''s storied history in Olympic table tennis. It reinforces the effectiveness of their robust national development program, which continuously produces world-class talent ready to dominate the international arena.', N'Olympics', 2, 1, 2, 2, CAST(N'2024-08-04T00:00:00.000' AS DateTime), N'https://images.pexels.com/photos/863988/pexels-photo-863988.jpeg?auto=compress&cs=tinysrgb&w=1200', 500),

(N'10', N'Curry shoots up France as Team USA takes another gold', N'A masterclass in three-point shooting.', CAST(N'2024-08-05T00:00:00.000' AS DateTime), N'Stephen Curry led Team USA to victory against France in the final match. His incredible performance, sinking consecutive three-pointers in the final quarter, secured another gold medal for the United States.

In what will be remembered as one of the greatest shooting exhibitions in Olympic history, Curry delivered under immense pressure. As France mounted a fierce comeback attempt in the fourth quarter, Curry responded with an astonishing flurry of deep shots that effectively sealed the game. The crowd was left stunned by his effortless release and limitless shooting range.

The victory also highlights the incredible depth and synergy of Team USA, which featured a roster packed with NBA superstars. Despite facing highly competitive international teams that have significantly closed the talent gap in recent years, the American squad''s defensive intensity and offensive firepower ultimately proved too much to handle.

This gold medal adds a crowning achievement to Curry''s already legendary career, filling one of the few remaining gaps in his trophy cabinet. As the celebrations unfold, basketball fans around the world are celebrating the sport''s continued growth and the spectacular entertainment it provides on the global stage.', N'Olympics', 3, 1, 1, 1, CAST(N'2024-08-05T00:00:00.000' AS DateTime), N'https://images.pexels.com/photos/209977/pexels-photo-209977.jpeg?auto=compress&cs=tinysrgb&w=1200', 890);
GO

USE [master]
GO
ALTER DATABASE [FUNewsManagement] SET  READ_WRITE 
GO
