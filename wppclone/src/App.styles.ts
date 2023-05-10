import styled from "styled-components";

export const Container = styled('div') `
    height: 100vh;
    background-color: #EDEDED;
    font-family: 'Segoe UI', Helvetica, 'Lucida Grande', Arial, sans-serif;
    display: flex;
`;

export const Sidebar = styled('div') `
    width: 35%;
    max-width: 415px;
    display: flex;
    flex-direction: column;

    .header {
        height: 60px;
        padding: 0 15px;
        display: flex;
        align-items: center;

        .header--profile {
            width: 40px;
            height: 40px;
            border-radius: 50%;
        }

        .header--buttons {
            flex: 1;
            display: flex;
            justify-content: end;
            align-items: center;
            gap: 3px;

            .header--btn {
                width: 40px;
                height: 40px;
                display: flex;
                justify-content: center;
                align-items: center;
                border-radius: 50%;
                cursor: pointer;

                &:hover {
                    background-color: #DDD;
                }
            }
        }
    }
`;

export const SearchArea = styled('div') `
    height: 50px;
    padding: 5px 15px;
    background-color: #F6F6F6;
    border-bottom: 1px solid #EEE;

    .search--input {
        height: 100%;
        padding: 0 10px;
        display: flex;
        align-items: center;
        background-color: #FFF;
        border-radius: 25px;
        overflow: hidden;

        input {
            flex: 1;
            height: 100%;
            padding: 5px;
            margin-left: 5px;
            outline: 0;
            border: 0;
        }
    }
`;

export const ChatList = styled('div') `
    flex: 1;
    background-color: #FFF;
    overflow-y: auto;
    
    &::-webkit-scrollbar {
        width: 6px;
    }
    &::-webkit-scrollbar-track {
        background-color: #EEE;
    }
    &::-webkit-scrollbar-thumb {
        background-color: rgba(0, 0, 0, 0.2);
        border-radius: 2px;
    }
`;

export const ContentArea = styled('div') `
    flex: 1;
`;