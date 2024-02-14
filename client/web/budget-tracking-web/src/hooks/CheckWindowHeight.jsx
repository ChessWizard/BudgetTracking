import { useEffect, useState } from "react";

// Pencere genişliğini kontrol eden bir hook
// Responsive bir biçimde css özellikleri verebilmemize yarar
function useWindowSize() {
  const [size, setSize] = useState([window.innerWidth, window.innerHeight]);
  useEffect(() => {
    const handleResize = () => {
      setSize([window.innerWidth, window.innerHeight]);
    };
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);
  return size;
}

export default useWindowSize;
