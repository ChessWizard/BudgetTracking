import React from "react";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { BsLinkedin, BsGithub, BsYoutube, BsInstagram } from "react-icons/bs";
import { Link } from "react-router-dom";

const Footer = () => {
  return (
    <>
      <footer>
        <div className="p-10 bg-slate-500 text-white">
          <div className="max-w-7xl mx-auto">
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-2">
              <div className="mb-5">
                <div className="flex items-center mb-3 md:mb-5 lg:mb-5">
                  <FaMoneyBillTrendUp size={"30"} color="white" />
                  <div className="text-xl font-semibold ml-3 mt-2 md:text-2xl lg:text-3xl">
                    Etkili Bütçe
                  </div>
                </div>
                <div className="mb-3 md:mb-5 lg:mb-5">
                  Adres: Namık Kemal Mah. Kampüs Cad. No:1
                  Süleymanpaşa/Tekirdağ, Türkiye
                </div>
                <Link className="mb-3 md:mb-5 lg:mb-5" to="tel:+90 1234567898">
                  Telefon: +90 1234567898
                </Link>
                <Link
                  className="mb-3 md:mb-5 lg:mb-5"
                  to="mailto:budget-tracking@gmail.com"
                >
                  Email: budget-tracking@gmail.com
                </Link>
                <div className="flex">
                  <Link className="text-white mx-2">
                    <BsLinkedin className="text-xl md:text-2xl lg:text-2xl" />
                  </Link>
                  <Link className="text-white mx-2">
                    <BsInstagram className="text-xl md:text-2xl lg:text-2xl" />
                  </Link>
                  <Link className="text-white mx-2">
                    <BsGithub className="text-xl md:text-2xl lg:text-2xl" />
                  </Link>
                  <Link className="text-white mx-2">
                    <BsYoutube className="text-xl md:text-2xl lg:text-2xl" />
                  </Link>
                </div>
              </div>
              <div className="mb-5">
                <div className="text-base mb-3 font-semibold mt-2 md:text-xl lg:text-2xl md:mb-5 lg:mb-5">
                  Bilgilendirme
                </div>
                <ul>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="privacy">
                      <span className="hover:underline">
                        Gizlilik Politikası
                      </span>
                    </Link>
                  </li>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="terms">
                      <span className="hover:underline">
                        Şartlar ve Koşullar
                      </span>
                    </Link>
                  </li>
                </ul>
              </div>
              <div className="mb-5">
                <div className="text-base mb-3 font-semibold mt-2 md:text-xl lg:text-2xl md:mb-5 lg:mb-5">
                  Hesap
                </div>
                <ul>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="about">
                      <span className="hover:underline">Hakkımızda</span>
                    </Link>
                  </li>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="questions">
                      <span className="hover:underline">
                        Sıkça Sorulan Sorular
                      </span>
                    </Link>
                  </li>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="contact">
                      <span className="hover:underline">İletişim</span>
                    </Link>
                  </li>
                </ul>
              </div>
              <div className="mb-5">
                <div className="text-base mb-3 font-semibold mt-2 md:text-xl lg:text-2xl md:mb-5 lg:mb-5">
                  Sık Kullanılanlar
                </div>
                <ul>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="addBudget">
                      <span className="hover:underline">Bütçe Ekle</span>
                    </Link>
                  </li>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="depts">
                      <span className="hover:underline">Borçlar</span>
                    </Link>
                  </li>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="depts">
                      <span className="hover:underline">Takvime Göre Borçlar</span>
                    </Link>
                  </li>
                  <li className="mb-3 md:mb-5 lg:mb-5">
                    <Link to="depts">
                      <span className="hover:underline">Grafikler</span>
                    </Link>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </>
  );
};

export default Footer;
