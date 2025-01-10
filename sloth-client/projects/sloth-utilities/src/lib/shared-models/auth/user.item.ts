import { UserRole } from "./user-role.item";

export interface User {
    userID: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    userRoles: UserRole[];
}