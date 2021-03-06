
USE [master]
GO
/****** Object:  Database [mrp]    Script Date: 4/12/2021 11:17:38 PM ******/
CREATE DATABASE [mrp]
 CONTAINMENT = NONE
GO
ALTER DATABASE [mrp] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mrp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mrp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mrp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mrp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mrp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mrp] SET ARITHABORT OFF 
GO
ALTER DATABASE [mrp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [mrp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mrp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mrp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mrp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mrp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mrp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mrp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mrp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mrp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [mrp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mrp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mrp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [mrp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [mrp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mrp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [mrp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mrp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [mrp] SET  MULTI_USER 
GO
ALTER DATABASE [mrp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mrp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [mrp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [mrp] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [mrp] SET DELAYED_DURABILITY = DISABLED 
GO
USE [mrp]
GO
/****** Object:  Table [dbo].[Dobavljac]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dobavljac](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](150) NULL,
    [Adresa] [varchar](100) NULL,
    [Grad] [varchar](50) NULL,
    [Drzava] [bigint] NULL,
    [Email] [varchar](50) NULL,
    [KontaktTelefon] [varchar](50) NULL,
 CONSTRAINT [PK_Dobavljac] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Drzava]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Drzava](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](50) NULL,
 CONSTRAINT [PK_Drzava] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Materijal]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Materijal](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](100) NULL,
    [Kolicina] [float] NULL,
    [CenaPoJediniciMere] [float] NULL,
    [JedinicaMere] [varchar](2) NULL,
    [UkupnaCena] [float] NULL,
    [Skladiste] [bigint] NULL,
    [Dobavljac] [bigint] NULL,
    [RokTrajanja] [date] NULL,
 CONSTRAINT [PK_Materijal] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Oprema]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Oprema](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](100) NULL,
    [DatumKupovine] [date] NULL,
    [GodisnjaAmortizacija] [float] NULL,
    [Dobavljac] [bigint] NULL,
    [PocetnaCena] [float] NULL,
    [TrenutnaVrednost] [float] NULL,
 CONSTRAINT [PK_Oprema] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proizvod]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proizvod](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](100) NOT NULL,
    [VremePripreme] [bigint] NOT NULL,
 CONSTRAINT [PK_Proizvod] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RadnoMesto]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RadnoMesto](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](100) NULL,
 CONSTRAINT [PK_RadnoMesto] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sastojak]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sastojak](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Kolicina] [float] NULL,
    [Materijal] [bigint] NULL,
    [Proizvod] [bigint] NULL,
 CONSTRAINT [PK_Sastojak] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Skladiste]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Skladiste](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Naziv] [varchar](100) NULL
 CONSTRAINT [PK_Skladiste] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Zaposleni]    Script Date: 4/12/2021 11:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Zaposleni](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Ime] [varchar](50) NULL,
    [Prezime] [varchar](50) NULL,
    [Plata] [float] NULL,
    [DatumRodjenja] [date] NULL,
    [Adresa] [varchar](100) NULL,
    [Grad] [varchar](50) NULL,
    [Drzava] [bigint] NULL,
    [RadnoMesto] [bigint] NULL,
    [Kontakt] [varchar](50) NULL,
 CONSTRAINT [PK_Zaposleni] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Dobavljac]  WITH CHECK ADD  CONSTRAINT [FK_Dobavljac_Drzava] FOREIGN KEY([Drzava])
REFERENCES [dbo].[Drzava] ([Id])
GO
ALTER TABLE [dbo].[Dobavljac] CHECK CONSTRAINT [FK_Dobavljac_Drzava]
GO
ALTER TABLE [dbo].[Materijal]  WITH CHECK ADD  CONSTRAINT [FK_Materijal_Dobavljac] FOREIGN KEY([Dobavljac])
REFERENCES [dbo].[Dobavljac] ([Id])
GO
ALTER TABLE [dbo].[Materijal] CHECK CONSTRAINT [FK_Materijal_Dobavljac]
GO
ALTER TABLE [dbo].[Materijal]  WITH CHECK ADD  CONSTRAINT [FK_Materijal_Skladiste] FOREIGN KEY([Skladiste])
REFERENCES [dbo].[Skladiste] ([Id])
GO
ALTER TABLE [dbo].[Materijal] CHECK CONSTRAINT [FK_Materijal_Skladiste]
GO
ALTER TABLE [dbo].[Oprema]  WITH CHECK ADD  CONSTRAINT [FK_Oprema_Dobavljac] FOREIGN KEY([Dobavljac])
REFERENCES [dbo].[Dobavljac] ([Id])
GO
ALTER TABLE [dbo].[Oprema] CHECK CONSTRAINT [FK_Oprema_Dobavljac]
GO
ALTER TABLE [dbo].[Sastojak]  WITH CHECK ADD  CONSTRAINT [FK_Sastojak_Materijal] FOREIGN KEY([Materijal])
REFERENCES [dbo].[Materijal] ([Id])
GO
ALTER TABLE [dbo].[Sastojak] CHECK CONSTRAINT [FK_Sastojak_Materijal]
GO
ALTER TABLE [dbo].[Sastojak]  WITH CHECK ADD  CONSTRAINT [FK_Sastojak_Sastojak] FOREIGN KEY([Proizvod])
REFERENCES [dbo].[Proizvod] ([Id])
GO
ALTER TABLE [dbo].[Sastojak] CHECK CONSTRAINT [FK_Sastojak_Sastojak]
GO

ALTER TABLE [dbo].[Zaposleni]  WITH CHECK ADD  CONSTRAINT [FK_Zaposleni_Drzava] FOREIGN KEY([Drzava])
REFERENCES [dbo].[Drzava] ([Id])
GO
ALTER TABLE [dbo].[Zaposleni] CHECK CONSTRAINT [FK_Zaposleni_Drzava]
GO
ALTER TABLE [dbo].[Zaposleni]  WITH CHECK ADD  CONSTRAINT [FK_Zaposleni_RadnoMesto] FOREIGN KEY([RadnoMesto])
REFERENCES [dbo].[RadnoMesto] ([Id])
GO
ALTER TABLE [dbo].[Zaposleni] CHECK CONSTRAINT [FK_Zaposleni_RadnoMesto]
GO
USE [master]
GO
ALTER DATABASE [mrp] SET  READ_WRITE 
GO
