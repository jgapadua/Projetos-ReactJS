import "./styles.css";
import logoImage from '../../assets/NetflixLogoSvg.svg'
import profileImage from '../../assets/ProfileIMG.svg'

const Header = ({ black }: any) => {
  return (
    <header className={black ? "black" : ""}>
      <div className="header--logo">
        <a href="">
          <img
            src={logoImage}
            alt="Netflix"
          ></img>
        </a>
      </div>
      <div className="header--user">
        <a href="/">
          <img
            src={profileImage}
            alt="user"
          />
        </a>
      </div>
    </header>
  );
};

export default Header;
