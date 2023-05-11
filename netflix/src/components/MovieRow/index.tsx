import { ICategory } from "../../Tmdb";
import "./styles.css";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import { useState } from "react";

interface IProps {
  poster_path: string;
  original_title: string;
}

const MovieRow = ({ title, items }: ICategory) => {
  const [scrollX, setScrollX] = useState(-400);

  //window.innerWidth = tamanho do monitor que o usuario tiver utilizando

  const handleLeftArrow = () => {
    let x = scrollX + Math.round(window.innerWidth / 2);
    if (x > 0) {
      x = 0;
    }
    setScrollX(x);
  };

  const handleRightArrow = () => {
    let x = scrollX - Math.round(window.innerWidth / 2);
    let listW = items.results.length * 150;
    if (window.innerWidth - listW > x) {
      x = window.innerWidth - listW - 60;
    }
    setScrollX(x);
  };
  return (
    <div className="movieRow">
      <h2>{title}</h2>
      <div
        className="movieRow--left"
        onClick={handleLeftArrow}
        style={{ fontSize: 50 }}
      >
        <NavigateBeforeIcon fontSize="large"></NavigateBeforeIcon>
      </div>
      <div
        className="movieRow--right"
        onClick={handleRightArrow}
        style={{ fontSize: 50 }}
      >
        <NavigateNextIcon fontSize="large"></NavigateNextIcon>
      </div>

      <div className="movieRow--listarea">
        <div
          className="movieRow--list"
          style={{
            marginLeft: scrollX,
            width: items.results.length * 150,
          }}
        >
          {items.results.map((item: IProps, key: any) => (
            <div key={key} className="movieRow--item">
              <img
                src={`https://image.tmdb.org/t/p/w300${item.poster_path}`}
                alt={item.original_title}
              />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default MovieRow;
