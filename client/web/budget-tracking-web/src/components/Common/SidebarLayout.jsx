import { Outlet } from "react-router-dom";
import Sidebar from "./Sidebar";

const SidebarLayout = () => {
  return (
    <>
      {/* Dinamik olarak gelecek olan içerikler Outlet ile işaretlenir ve ne gelirse render olur */}
      {/* Sidebar horizontal bir içerik olduğundan dolayı tüm görünümü flex ile tek satıra geitirip yan yana yapıyoruz */}
      <div className="flex">
        <Sidebar />
        <Outlet />
      </div>
    </>
  );
};

export default SidebarLayout;
