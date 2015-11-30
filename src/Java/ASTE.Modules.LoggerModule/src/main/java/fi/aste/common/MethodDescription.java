package fi.aste.common;

/**
 * Created with IntelliJ IDEA.
 * User: vmm
 * Date: 27.11.2015
 * Time: 23:13
 * Copyright 2015 Oy Aurora Data and Systems Ltd.
 */
public class MethodDescription {
    private String method = null;
    private String params = null;

    public MethodDescription(String method, String params) {
        this.method = method;
        this.params = params;
    }

    public String getMethod() {
        return method;
    }

    public String getParams() {
        return params;
    }
}
