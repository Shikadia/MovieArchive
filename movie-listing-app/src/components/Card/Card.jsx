import React from "react";
import styles from "./card.module.css";
import { Button, Menu } from "@mantine/core";
import { Link } from "react-router-dom";

const Card = ({ id, name, viewLink, editLink, deleteFunc }) => {
  return (
    <div className={styles.container}>
      <h1 className={styles.title}>{name}</h1>
      <p className={styles.description}></p>

      <Menu shadow="md" width={200}>
        <Menu.Target className={styles.toggle}>
          <img src="/dots.png" alt="" className={styles.menu} />
        </Menu.Target>

        <Menu.Dropdown className={styles.mantine_dropdown}>
          <Menu.Item className={styles.dropdown_item}>
            <Link to={viewLink} className={styles.link}>
              View
            </Link>
          </Menu.Item>
          <Menu.Item className={styles.dropdown_item}>
            <Link to={editLink} className={styles.link}>
              Edit
            </Link>
          </Menu.Item>
          <Menu.Item className={styles.dropdown_item} onClick={deleteFunc}>
            Delete
          </Menu.Item>
        </Menu.Dropdown>
      </Menu>
      {/* <div className="first-button-container">
        <button className="aboutus-button">About Us</button>
        <button className="contactus-button"></button>
      </div> */}
    </div>
  );
};

export default Card;
