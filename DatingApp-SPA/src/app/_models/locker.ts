export interface Locker {
    id: number;
    lockerId: number;
    lockerUserId: number;
    lockerVacant: boolean;
    lockerBusy: boolean;
    lockerCheckIn: Date;
    lockerCheckOut: Date;
}
