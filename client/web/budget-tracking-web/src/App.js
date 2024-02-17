import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Common/Layout";
import Home from "./pages/Home";
import "./App.css";
import Login from "./pages/Login";
import Register from "./pages/Register";
import SidebarLayout from "./components/Common/SidebarLayout";
import Expense from "./pages/Expense";
import Profile from "./pages/Profile";
import ExpenseReport from "./pages/ExpenseReport";
import Category from "./pages/Category";
import PaymentAccount from "./pages/PaymentAccount";
import Calendar from "./pages/Calendar";
import Chart from "./pages/Chart";
import Budget from "./pages/Budget";
import Planned from "./pages/Planned";
import AuthenticatedRoutes from "./routes/AuthenticatedRoutes";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "./features/auth/authSlice";

function App() {
  const accessToken = useSelector(selectCurrentToken);
  console.log("Token: " + accessToken);

  return (
    <>
      <BrowserRouter>
        <Routes>
          {/* Normal Layout Screens Start */}
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
          </Route>
          {/* Normal Layout Screens End */}

          {/* Sidebar Layout Screens End */}

          {/* No Layout Screeens Start */}
          <Route>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Route>
          {/* No Layout Screeens End */}
        </Routes>

        {/* Sidebar Layout Screens Start */}
        <AuthenticatedRoutes isAuthenticated={accessToken === null ? false : true}>
          <Route path="/dashboard" element={<SidebarLayout />}>
            {/* dashboard uzantısına sahip her yerde sidebar çıkar */}
            <Route index path="/dashboard/expense" element={<Expense />} />
            <Route path="/dashboard/payment" element={<PaymentAccount />} />
            <Route path="/dashboard/planned" element={<Planned />} />
            <Route path="/dashboard/budget" element={<Budget />} />
            <Route path="/dashboard/chart" element={<Chart />} />
            <Route path="/dashboard/calendar" element={<Calendar />} />
            <Route path="/dashboard/category" element={<Category />} />
            <Route path="/dashboard/account" element={<Profile />} />
            <Route
              path="/dashboard/report/transaction"
              element={<ExpenseReport />}
            />
          </Route>
        </AuthenticatedRoutes>
      </BrowserRouter>
    </>
  );
}

export default App;
