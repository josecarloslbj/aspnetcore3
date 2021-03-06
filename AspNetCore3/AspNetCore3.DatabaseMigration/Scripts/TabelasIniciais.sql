GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[Endereco](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoEndereco] [int] NULL,
	[Logradouro] [varchar](500) NOT NULL,
	[Numero] [varchar](50) NULL,
	[Complemento] [varchar](500) NULL,
	[Cep] [varchar](50) NULL,
	[Cidade] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Pais] [varchar](50) NULL,
	[Status] [int] NOT NULL,
	[DataInicio] [datetime] NOT NULL,
	[DataFim] [datetime] NULL,
 CONSTRAINT [PK_Endereco] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[Pessoa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPessoa] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[DataInicio] [datetime] NOT NULL,
	[DataFim] [datetime] NULL,
 CONSTRAINT [PK_Pessoa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PessoaFisica]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[PessoaFisica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NULL,
	[Idade] [int] NULL,
	[IdPessoa] [int] NULL,
 CONSTRAINT [PK_PessoaFisica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PessoaJuridica]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[PessoaJuridica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPessoa] [int] NOT NULL,
	[Cnpj] [varchar](14) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TB_JOGADOR]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[TB_JOGADOR](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NM_JOGADOR] [varchar](100) NOT NULL,
	[NR_IDADE] [int] NOT NULL,
	[ID_TIME] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TB_TIME]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[TB_TIME](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NM_TIME] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Telefone]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[Telefone](
	[Id] [int] NULL,
	[Area] [varchar](50) NOT NULL,
	[Ddd] [varchar](50) NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[IdUsuario] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoEndereco]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[TipoEndereco](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Descricao] [varchar](500) NULL,
	[Status] [bit] NOT NULL,
	[DataInicio] [datetime] NOT NULL,
	[DataFim] [datetime] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_TipoEndereco] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoPessoa]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[TipoPessoa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Descricao] [varchar](500) NOT NULL,
	[Status] [bit] NOT NULL,
	[DataInicio] [datetime] NOT NULL,
	[DataFim] [datetime] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_TipoPessoa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoTelefone]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[TipoTelefone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Nome] [varchar](50) NULL,
	[Descricao] [varchar](500) NOT NULL,
	[Status] [bit] NOT NULL,
	[DataInicio] [datetime] NOT NULL,
	[DataFim] [datetime] NULL,
 CONSTRAINT [PK_TipoTelefone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 01/12/2019 15:29:35 ******/

CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPessoa] [int] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[Senha] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK_Pessoa_Pessoa] FOREIGN KEY([IdTipoPessoa])
REFERENCES [dbo].[TipoPessoa] ([Id])
GO
ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK_Pessoa_Pessoa]
GO
ALTER TABLE [dbo].[PessoaJuridica]  WITH CHECK ADD  CONSTRAINT [FK_PessoaJuridica_Pessoa] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[PessoaJuridica] CHECK CONSTRAINT [FK_PessoaJuridica_Pessoa]
GO
ALTER TABLE [dbo].[TB_JOGADOR]  WITH CHECK ADD  CONSTRAINT [FK_TB_TIME] FOREIGN KEY([ID_TIME])
REFERENCES [dbo].[TB_TIME] ([ID])
GO
ALTER TABLE [dbo].[TB_JOGADOR] CHECK CONSTRAINT [FK_TB_TIME]
GO
ALTER TABLE [dbo].[Telefone]  WITH CHECK ADD  CONSTRAINT [FK_Telefone_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Telefone] CHECK CONSTRAINT [FK_Telefone_Usuario]
GO
ALTER TABLE [dbo].[TipoEndereco]  WITH CHECK ADD  CONSTRAINT [FK_TipoEndereco_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[TipoEndereco] CHECK CONSTRAINT [FK_TipoEndereco_Usuario]
GO
ALTER TABLE [dbo].[TipoPessoa]  WITH CHECK ADD  CONSTRAINT [FK_TipoPessoa_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[TipoPessoa] CHECK CONSTRAINT [FK_TipoPessoa_Usuario]
GO
ALTER TABLE [dbo].[TipoTelefone]  WITH CHECK ADD  CONSTRAINT [FK_TipoTelefone_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[TipoTelefone] CHECK CONSTRAINT [FK_TipoTelefone_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_PessoaFisica] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[PessoaFisica] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_PessoaFisica]
GO
