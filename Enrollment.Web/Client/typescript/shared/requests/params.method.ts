import { URLSearchParams } from "@angular/http";

export const requestParams = () =>
{
    var params = new URLSearchParams();
    params.set("__RequestVerificationToken", $("input[type='hidden'][name='__RequestVerificationToken']").val());
    return params;
};