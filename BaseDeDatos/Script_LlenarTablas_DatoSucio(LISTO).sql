USE ProyectoPrograAvanzada
GO

---*** Insert pre-cargados ***---

INSERT INTO UnidadMedida
(SiglasUni, DescUnidad)
VALUES ('USD', 'Dolares'),
('CRC', 'Colones'),
('JPY', 'Yen');
GO

INSERT INTO Periodo
(SiglasPer, DescPeriodo)
VALUES ('D', 'Diario'),
('S', 'Semanal'),
('M', 'Mensual'),
('T', 'Trimestral');
GO

INSERT INTO Estado
(SiglasEst, DescEstado)
VALUES ('ACTI', 'ACTIVO'),
('PASI', 'PASIVO'),
('PATR', 'PATRIMONIO');
GO

INSERT INTO Metadata
(SiglasMet, NomMetadata, DescMetadata)
VALUES ('BA_', 'Banco', 'Entidad bancaria'),
('CA_','Carro', 'Transporte'),
('MO_', 'Motocicleta', 'Transporte');
GO

---*** Guarda Posvalor segun metadata ***---

INSERT INTO Posvalor
(SiglasMet, SiglasPos, NomPosvalor, DescPosvalor)
VALUES ('CA_', 'TOY','TOYOTA', 'AUTOMOVIL'),
('CA_', 'HYU','HYUNDAI', 'AUTOMOVIL'),
('CA_', 'KIA','KIA', 'AUTOMOVIL'),
('CA_', 'NIS','NISSAN', 'AUTOMOVIL'),
('BA_', 'BAC','BAC SAN JOSE', 'ENTIDAD BANCARIA'),
('BA_', 'BCR','BANCO DE COSTA RICA', 'ENTIDAD BANCARIA'),
('BA_', 'BN','BANCO NACIONAL', 'ENTIDAD BANCARIA'),
('MO_', 'YAM','YAMAHA', 'MOTOCICLETA'),
('MO_', 'KTM','KTM', 'MOTOCICLETA');
GO