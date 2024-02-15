import { FaMoneyBill1Wave } from "react-icons/fa6";
import { IoCalendar } from "react-icons/io5";
import { IoPieChart } from "react-icons/io5";
import { IoIosAddCircle } from "react-icons/io";
import { FaUser } from "react-icons/fa";
import { FaUserPlus } from "react-icons/fa6";
import { FaCashRegister } from "react-icons/fa6";
import { PiSquaresFourFill } from "react-icons/pi";
import { GrMoney } from "react-icons/gr";
import { MdSchedule } from "react-icons/md";
import { MdCategory } from "react-icons/md";

const getIconByLabel = (label) => {
  switch (label) {
    case "Harcamalarım":
      return <FaMoneyBill1Wave size={"15"} color="#FFFAFA" />;
    case "Takvim":
      return <IoCalendar size={"15"} color="#FFFAFA" />;
    case "Grafik":
      return <IoPieChart size={"15"} color="#FFFAFA" />;
    case "Ekle":
      return <IoIosAddCircle size={"15"} color="#FFFAFA" />;
    default:
      break;
  }
};

const getRouteByLabel = (label) => {
  switch (label) {
    case "Harcamalarım":
      return "dashboard/expense";
    case "Takvim":
      return "dashboard/calendar";
    case "Grafik":
      return "dashboard/chart";
    case "Ekle":
      return "dashboard/budget";
    default:
      break;
  }
};

const getSidebarIconsByLabel = (label) => {
  switch (label) {
    case "Genel Bakış":
      return <PiSquaresFourFill size={"30"} color="white" />;
    case "İşlemler":
      return <GrMoney size={"25"} color="white" />
    case "Hesaplar":
      return <FaMoneyBill1Wave size={"25"} color="white" /> 
    case "Planlanmış Ödemeler":
      return <MdSchedule size={"30"} color="white" />
    case "Bütçeler":
      return <FaCashRegister size={"25"} color="white" />
    case "Grafikler":
      return <IoPieChart size={"25"} color="white" />
    case "Takvim":
      return <IoCalendar size={"25"} color="white" />
    case "Kategori Yönetimi":
      return <MdCategory size={"25"} color="white" />  
    default:
      break;
  }
};

const getSidebarRouteByLabel = (label) => {
  switch (label) {
    case "Genel Bakış":
      return "/dashboard"
    case "İşlemler":
      return "/dashboard/expense"
    case "Hesaplar":
      return "/dashboard/payment"
    case "Planlanmış Ödemeler":
      return "/dashboard/planned"
    case "Bütçeler":
      return "/dashboard/budget"
    case "Grafikler":
      return "/dashboard/chart"
    case "Takvim":
      return "/dashboard/calendar"
    case "Kategori Yönetimi":
      return "/dashboard/category"
    default:
      break;
  }
}

const getNotLoggedInIconByLabel = (label) => {
  switch (label) {
    case "Giriş Yap":
      return <FaUser size={"15"} color="#FFFAFA" />;
    case "Üye Ol":
      return <FaUserPlus size={"20"} color="#FFFAFA" />;
    default:
      break;
  }
};
const getNotLoggedInRouteByLabel = (label) => {
  switch (label) {
    case "Giriş Yap":
      return "/login";
    case "Üye Ol":
      return "/register";
    default:
      break;
  }
};

const getCategoryIcons = () => {
  return [
    "https://webapp.fastbudget.app/static/icons/ic_audit_base.svg",
    "https://webapp.fastbudget.app/static/icons/ic_bank.svg",
    "https://webapp.fastbudget.app/static/icons/ic_base_health.svg",
    "https://webapp.fastbudget.app/static/icons/ic_base_incomes.svg",
    "https://webapp.fastbudget.app/static/icons/ic_base_pet_food.svg"
  ]
}

export {
  getIconByLabel,
  getSidebarRouteByLabel,
  getNotLoggedInIconByLabel,
  getNotLoggedInRouteByLabel,
  getRouteByLabel,
  getSidebarIconsByLabel,
  getCategoryIcons
};
