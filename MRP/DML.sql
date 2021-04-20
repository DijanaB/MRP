insert into Drzava(Naziv)
values('Republika Srbija')

insert into Drzava(Naziv)
values('Republika Hrvatska')

insert into Drzava(Naziv)
values('Bosna i Hercegoina')

insert into Drzava(Naziv)
values('Makedonija')

select * from Drzava

insert into Dobavljac(Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon)
values('AD Imlek','Industrijsko naselje bb, Padinska skela','Beograd',1,'info@imlek.rs','+381 11 30 50 505')

insert into Dobavljac(Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon)
values('DANUBIUS d.o.o.','Kanalska 1','Novi Sad',1,'office@danubius.rs','+381 21 48 08 900')

insert into Dobavljac(Naziv,Adresa,Grad,Drzava,KontaktTelefon)
values('AD VRENJE d.o.o.','Radnička 3', 'Beograd',1,'(011) 305 81 00')

insert into Dobavljac(Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon)
values('Pekarska i poslastičarksa oprema Veselinovic','Braće Vučkovića br. 2 - Žarkovo', 'Beograd',1,'veselinovici.doo@gmail.com','+381 (0) 15/354-739')



select * from Dobavljac

insert into Oprema(Naziv,DatumKupovine,GodisnjaAmortizacija,Dobavljac,PocetnaCena,TrenutnaVrednost)
values('Izlozbena vitrina 1','2020-05-10',5000,4,70000,65000)

insert into Oprema(Naziv,DatumKupovine,GodisnjaAmortizacija,Dobavljac,PocetnaCena,TrenutnaVrednost)
values('Izlozbena vitrina 2','2020-05-10',5000,4,70000,65000)

insert into Oprema(Naziv,DatumKupovine,GodisnjaAmortizacija,Dobavljac,PocetnaCena,TrenutnaVrednost)
values('Mikser za kolace','2020-05-10',5000,4,40000,35000)

insert into Oprema(Naziv,DatumKupovine,GodisnjaAmortizacija,Dobavljac,PocetnaCena,TrenutnaVrednost)
values('Pekarska pec','2020-05-10',10000,4,120000,110000)

select * from Oprema

insert into RadnoMesto(Naziv)
values('Prodavac')

insert into RadnoMesto(Naziv)
values('Poslasticar')

select * from RadnoMesto

insert into Zaposleni(Ime,Prezime,Plata,DatumRodjenja,Adresa,Grad,Drzava,RadnoMesto,Kontakt)
values('Zorica','Babic',50000,'1996-08-07','Mice Radakovica 86','Apatin',1,1,'0635478201')

insert into Zaposleni(Ime,Prezime,Plata,DatumRodjenja,Adresa,Grad,Drzava,RadnoMesto,Kontakt)
values('Dijana','Bursac',50000,'1996-08-07','Mice Radakovica 86','Apatin',1,2,'0636678501')

select * from Zaposleni

insert into Skladiste(Naziv,Broj)
values ('Skladiste za kolace',100)

insert into Skladiste(Naziv,Broj)
values ('Skladiste za peciva',101)

insert into Skladiste(Naziv,Broj)
values ('Skladiste materijala',102)

select * from Skladiste

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Brasno tip 400 ostro',250,55,'kg',13750,'2021-12-12',3,2)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Brasno tip 400 meko',250,55,'kg',13750,'2021-12-12',3,2)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Brasno tip 500 meko',300,56,'kg',16800,'2021-12-12',3,2)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Svezi kvasac',10,85,'kg',850,'2021-06-12',3,3)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Dugotrajno mleko',50,100,'l',1000,'2021-10-12',3,1)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Brasno tip 400 ostro',250,55,'kg',13750,'2021-12-12',3,2)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Brasno tip 400 meko',250,55,'kg',13750,'2021-12-12',3,2)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Brasno tip 500 meko',300,56,'kg',16800,'2021-12-12',3,2)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Svezi kvasac',10,85,'kg',850,'2021-06-12',3,3)

insert into Materijal(Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,RokTrajanja,Skladiste,Dobavljac)
values('Dugotrajno mleko',50,100,'l',1000,'2021-10-12',3,1)

select * from Materijal

select * from Proizvod

insert into Proizvod(Naziv,VremePripreme)
values('Slatke kiflice','2021-05-01')

insert into Proizvod(Naziv,VremePripreme)
values('Krofne','2021-05-01')

insert into Proizvod(Naziv,VremePripreme)
values('Bajadera','2021-05-01')

insert into Proizvod(Naziv,VremePripreme)
values('Dobos torta','2021-05-01')


insert into Sastojak(Kolicina,Materijal,Proizvod)
values(2,5,1)

insert into Sastojak(Kolicina,Materijal,Proizvod)
values(2,2,1)

insert into Sastojak(Kolicina,Materijal,Proizvod)
values(0.5,4,1)

select * from Sastojak

insert into FazaProizvodnje(Naziv)
values('Priprema')

insert into FazaProizvodnje(Naziv)
values('Pecenje')

insert into FazaProizvodnje(Naziv)
values('Hladjenje')

select * from FazaProizvodnje

select * from RadnoMesto

insert into VremenaProizvodnje(DatumVreme,FazaProizvodnje,RadnoMesto,Proizvod)
values('2021-05-05 15:00',1,2,1)

insert into VremenaProizvodnje(DatumVreme,FazaProizvodnje,RadnoMesto,Proizvod)
values('2021-05-05 16:00',2,2,1)

insert into VremenaProizvodnje(DatumVreme,FazaProizvodnje,RadnoMesto,Proizvod)
values('2021-05-05 16:15',3,2,1)

select * from VremenaProizvodnje






