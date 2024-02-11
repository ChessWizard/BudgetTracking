import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Common/Layout";
import Home from "./pages/Home";
import "./App.css";
import Login from "./pages/Login";
import Register from "./pages/Register";
import SidebarLayout from "./components/Common/SidebarLayout";
import Expense from "./pages/Expense";
import Profile from "./pages/Profile";
import RequireAuth from "./components/Common/RequireAuth";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          {/* Normal Layout Screens Start */}
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
          </Route>
          {/* Normal Layout Screens End */}

          {/* Sidebar Layout Screens Start */}
          <Route path="/dashboard" element={<RequireAuth><SidebarLayout /></RequireAuth>}>{/* dashboard uzantısına sahip her yerde sidebar çıkar */}
            <Route index path="/dashboard/expense" element={<RequireAuth><Expense /></RequireAuth>}/>
            <Route path="/dashboard/account" element={<RequireAuth><Profile /></RequireAuth>} />
          </Route>
          {/* Sidebar Layout Screens End */}

          {/* No Layout Screeens Start */}
          <Route>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Route>
          {/* No Layout Screeens End */}
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
