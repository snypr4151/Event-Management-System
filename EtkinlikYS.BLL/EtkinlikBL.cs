﻿using DAL;
using EtkinlikYS.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EtkinlikYS.BLL
{
    public class EtkinlikBL
    {
            public bool EtkinlikKayit(Etkinlik etkinlik)
            {
                try
                {
                if (etkinlik.MevcutKontejan > etkinlik.ToplamKontejan)
                {
                    throw new Exception("Mevcut katılımcı sayısı kontenjan değerinden fazla olamaz.");
                }

                SqlParameter[] p = {
                    new SqlParameter("@EtkinlikAdi", etkinlik.EtkinlikAdi ?? (object)DBNull.Value),
                    new SqlParameter("@Fiyat", etkinlik.Fiyat ?? (object)DBNull.Value),
                    new SqlParameter("@EtkinlikTuru", etkinlik.EtkinlikTuru ?? (object)DBNull.Value),
                    new SqlParameter("@ToplamKontejan", etkinlik.ToplamKontejan),
                    new SqlParameter("@MevcutKontejan", etkinlik.MevcutKontejan),
                    new SqlParameter("@EtkinlikTarihi", etkinlik.EtkinlikTarihi),
                    new SqlParameter("@EtkinlikYeri", etkinlik.EtkinlikYeri ?? (object)DBNull.Value),
                    new SqlParameter("@Aciklama", etkinlik.Aciklama ?? (object)DBNull.Value),
                    new SqlParameter("@Resim", etkinlik.Resim ?? (object)DBNull.Value),
                    new SqlParameter("@OlusturanKullaniciID", etkinlik.OlusturanKullaniciID)
                };

                    var hlp = Helper.SDP;
                    return hlp.ExecuteNonQuery("Insert into Etkinlikler (EtkinlikAdi, Fiyat, EtkinlikTuru, ToplamKontejan, MevcutKontejan, EtkinlikTarihi, EtkinlikYeri, Aciklama, Resim, OlusturanKullaniciID) values (@EtkinlikAdi, @Fiyat, @EtkinlikTuru, @ToplamKontejan, @MevcutKontejan, @EtkinlikTarihi, @EtkinlikYeri, @Aciklama, @Resim, @OlusturanKullaniciID)", p) > 0;
                }
                catch (SqlException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }


        public List<Etkinlik> EtkinlikleriGetir()
        {
            List<Etkinlik> etkinlikler = new List<Etkinlik>();
            try
            {
                SqlParameter[] p = { };

                var hlp = Helper.SDP;
                var dr = hlp.ExecuteReader("SELECT * FROM Etkinlikler", p);

                while (dr.Read())
                {
                    Etkinlik etkinlik = new Etkinlik
                    {
                        EtkinlikID = Convert.ToInt32(dr["EtkinlikID"]),
                        EtkinlikAdi = dr["EtkinlikAdi"].ToString(),
                        Fiyat = dr["Fiyat"].ToString(),
                        EtkinlikTuru = dr["EtkinlikTuru"].ToString(),
                        ToplamKontejan = Convert.ToInt32(dr["ToplamKontejan"]),
                        MevcutKontejan = Convert.ToInt32(dr["MevcutKontejan"]),
                        EtkinlikTarihi = Convert.ToDateTime(dr["EtkinlikTarihi"]),
                        EtkinlikYeri = dr["EtkinlikYeri"].ToString(),
                        Aciklama = dr["Aciklama"].ToString(),
                        Resim = dr["Resim"] as byte[],
                        OlusturanKullaniciID = Convert.ToInt32(dr["OlusturanKullaniciID"])
                    };
                    etkinlikler.Add(etkinlik);
                }
                dr.Close();
                return etkinlikler;
            }
            catch (SqlException ex)
            {
                throw new Exception("Veritabanı hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                Helper.SDP.DisposeEt();
            }
        }

        public List<Etkinlik> KullaniciEtkinlikleriGetir(int kullaniciID)
        {
            List<Etkinlik> etkinlikler = new List<Etkinlik>();
            try
            {
                SqlParameter[] p = { new SqlParameter("@OlusturanKullaniciID", kullaniciID) };

                var hlp = Helper.SDP;
                var dr = hlp.ExecuteReader("SELECT * FROM Etkinlikler WHERE OlusturanKullaniciID = @OlusturanKullaniciID", p);

                while (dr.Read())
                {
                    Etkinlik etkinlik = new Etkinlik
                    {
                        EtkinlikID = Convert.ToInt32(dr["EtkinlikID"]),
                        EtkinlikAdi = dr["EtkinlikAdi"].ToString(),
                        Fiyat = dr["Fiyat"].ToString(),
                        EtkinlikTuru = dr["EtkinlikTuru"].ToString(),
                        ToplamKontejan = Convert.ToInt32(dr["ToplamKontejan"]),
                        MevcutKontejan = Convert.ToInt32(dr["MevcutKontejan"]),
                        EtkinlikTarihi = Convert.ToDateTime(dr["EtkinlikTarihi"]),
                        EtkinlikYeri = dr["EtkinlikYeri"].ToString(),
                        Aciklama = dr["Aciklama"].ToString(),
                        Resim = dr["Resim"] as byte[],
                        OlusturanKullaniciID = Convert.ToInt32(dr["OlusturanKullaniciID"])
                    };
                    etkinlikler.Add(etkinlik);
                }
                dr.Close();
                return etkinlikler;
            }
            catch (SqlException ex)
            {
                throw new Exception("Veritabanı hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                Helper.SDP.DisposeEt();
            }
        }


        public bool EtkinlikGuncelle(Etkinlik etkinlik)
        {
            try
            {
                SqlParameter[] p = {
                    new SqlParameter("@EtkinlikID", etkinlik.EtkinlikID),
                    new SqlParameter("@EtkinlikAdi", etkinlik.EtkinlikAdi ?? (object)DBNull.Value),
                    new SqlParameter("@Fiyat", etkinlik.Fiyat ?? (object)DBNull.Value),
                    new SqlParameter("@EtkinlikTuru", etkinlik.EtkinlikTuru ?? (object)DBNull.Value),
                    new SqlParameter("@ToplamKontejan", etkinlik.ToplamKontejan),
                    new SqlParameter("@EtkinlikTarihi", etkinlik.EtkinlikTarihi),
                    new SqlParameter("@EtkinlikYeri", etkinlik.EtkinlikYeri ?? (object)DBNull.Value),
                    new SqlParameter("@Aciklama", etkinlik.Aciklama ?? (object)DBNull.Value),
                    new SqlParameter("@Resim", etkinlik.Resim ?? (object)DBNull.Value)
                };

                var hlp = Helper.SDP;
                return hlp.ExecuteNonQuery("UPDATE Etkinlikler SET EtkinlikAdi = @EtkinlikAdi, Fiyat = @Fiyat, EtkinlikTuru = @EtkinlikTuru, ToplamKontejan = @ToplamKontejan, EtkinlikTarihi = @EtkinlikTarihi, EtkinlikYeri = @EtkinlikYeri, Aciklama = @Aciklama, Resim = @Resim WHERE EtkinlikID = @EtkinlikID", p) > 0;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool EtkinligeKatilimEkle(Etkinlik etkinlik, Kullanici kullanici)
        {
            try
            {
                var hlp = Helper.SDP;

                SqlParameter[] checkParams = {
            new SqlParameter("@EtkinlikID", etkinlik.EtkinlikID),
            new SqlParameter("@KullaniciID", kullanici.Kullaniciid)
        };

                using (var dr = hlp.ExecuteReader("SELECT COUNT(*) FROM EtkinlikKatilim WHERE EtkinlikID = @EtkinlikID AND KullaniciID = @KullaniciID", checkParams))
                {
                    if (dr.Read())
                    {
                        int katilimSayisi = dr.GetInt32(0);
                        if (katilimSayisi > 0)
                        {
                            throw new Exception("Bu etkinliğe zaten katıldınız.");
                        }
                    }
                }

                if (etkinlik.OlusturanKullaniciID == kullanici.Kullaniciid)
                {
                    throw new Exception("Kendi oluşturduğunuz etkinliğe katılamazsınız.");
                }

                decimal etkinlikFiyati = decimal.Parse(etkinlik.Fiyat);
                if (etkinlikFiyati > 0)
                {
                    if (kullanici.Bakiye < etkinlikFiyati)
                    {
                        throw new Exception("Yetersiz bakiye.");
                    }

                    kullanici.Bakiye -= etkinlikFiyati;

                    SqlParameter[] pBakiye = {
                new SqlParameter("@KullaniciID", kullanici.Kullaniciid),
                new SqlParameter("@Bakiye", kullanici.Bakiye)
            };

                    if (hlp.ExecuteNonQuery("UPDATE Kullanicilar SET Bakiye = @Bakiye WHERE KullaniciId = @KullaniciID", pBakiye) <= 0)
                    {
                        throw new Exception("Bakiye güncellenemedi.");
                    }
                }

                SqlParameter[] insertParams = {
            new SqlParameter("@EtkinlikID", etkinlik.EtkinlikID),
            new SqlParameter("@KullaniciID", kullanici.Kullaniciid)
        };

                bool result = hlp.ExecuteNonQuery("INSERT INTO EtkinlikKatilim (EtkinlikID, KullaniciID) VALUES (@EtkinlikID, @KullaniciID)", insertParams) > 0;

                if (result)
                {
                    SqlParameter[] pKontenjan = {
                new SqlParameter("@EtkinlikID", etkinlik.EtkinlikID)
            };
                    result = hlp.ExecuteNonQuery("UPDATE Etkinlikler SET MevcutKontejan = MevcutKontejan + 1 WHERE EtkinlikID = @EtkinlikID", pKontenjan) > 0;
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception("Veritabanı hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu: " + ex.Message);
            }
        }

    }

}
