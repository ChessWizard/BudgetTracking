import { RxHamburgerMenu } from "react-icons/rx";
import { FaMoneyBill1Wave } from "react-icons/fa6";
import { IoCalendar } from "react-icons/io5";
import { IoPieChart } from "react-icons/io5";
import { IoIosAddCircle } from "react-icons/io";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { FaUser } from "react-icons/fa";
import { FaUserPlus } from "react-icons/fa6";


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

const getNotLoggedInIconByLabel = (label) => {
    switch (label) {
        case "Giriş Yap":
            return <FaUser size={"15"} color="#FFFAFA"/>
        case "Üye Ol":
            return <FaUserPlus size={"20"} color="#FFFAFA"/>
        default:
            break;
    }
}

export {getIconByLabel, getNotLoggedInIconByLabel}