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
import ExpenseReport from "./pages/ExpenseReport";
import Category from "./pages/Category";
import PaymentAccount from "./pages/PaymentAccount";

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
            <Route path="/dashboard/category" element={<RequireAuth><Category /></RequireAuth>} />
            <Route path="/dashboard/payment" element={<RequireAuth><PaymentAccount /></RequireAuth>} />
            <Route path="/dashboard/report/transaction" element={<RequireAuth><ExpenseReport /></RequireAuth>} />
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
