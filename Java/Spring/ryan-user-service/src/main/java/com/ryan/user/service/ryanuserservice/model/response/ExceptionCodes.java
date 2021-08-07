package com.ryan.user.service.ryanuserservice.model.response;

import org.springframework.http.HttpStatus;

public class ExceptionCodes {
    // HTTP No Content
    public static int NoUserContent = HttpStatus.NO_CONTENT.value() * 100 + 1;

    // HTTP Not Found
    public static int UserNotFound = HttpStatus.NOT_FOUND.value() * 100 + 1;

    // HTTP Internal Server Error
    public static int UnknownError = HttpStatus.INTERNAL_SERVER_ERROR.value() * 100 + 1;
    public static int DuplicateUsersFound = HttpStatus.INTERNAL_SERVER_ERROR.value() * 100 + 2;
}
