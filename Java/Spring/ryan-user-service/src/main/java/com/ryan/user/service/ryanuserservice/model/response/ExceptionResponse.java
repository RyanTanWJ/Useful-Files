package com.ryan.user.service.ryanuserservice.model.response;

import java.util.HashMap;

public class ExceptionResponse extends BaseResponse {
    protected int code;
    protected HashMap<String, Object> metadata;

    public ExceptionResponse(int code, String message) {
        this.success = false;
        this.code = code;
        this.message = message;
    }

    public ExceptionResponse(int code, String message, HashMap<String, Object> metadata) {
        this.success = false;
        this.code = code;
        this.message = message;
        this.metadata = metadata;
    }

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }

    public HashMap<String, Object> getMetadata() {
        return metadata;
    }

    public void setMetadata(HashMap<String, Object> metadata) {
        this.metadata = metadata;
    }
}
