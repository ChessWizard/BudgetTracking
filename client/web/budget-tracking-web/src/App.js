import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Common/Layout";
import Home from "./pages/Home";
import "./App.css";
import Login from "./pages/Login";
import Register from "./pages/Register";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
          </Route>
          <Route>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
        </Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
