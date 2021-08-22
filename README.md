# BindNineCore
bind9 web utility (auto config writing, dns management, etc)

## bind9 configuration writer

WARNING: this does not keep your .named nor zones db. It will read the zones from the database on the Apply functionality.

It's written in C# (9.0) + .NET 5.0/Blazor.

The functionality is in `BindNineCore`, abstracted from `BindNineCore.Abstraction`.

The frontend is written in Blazor Server-Side (`BindNineCore.Web`), but it merely does anything but consume the API.

There is no authentication. You can build your own or integrate.

MariaDB is being used (please check the `ConnectionString` in `appsettings.json`.

This project is being used as a tool on my home environment, and I don't see any issues by using in a production environment.

## Recommended Environment

A recommended environment would be inside a container, with a shared volume between bind9's /etc/bind9, then using the mount point on the `appsettings.json`

## Missing content

I think CNAME is not working as expected.

At the moment only Aliases and NS are working. It is not hard to make all record types known by humanity though.

## Templating

Please see the templating system (`named.template` and `zone.template`) as they are the configuration file template that BindNineCore uses to generate the final configuration file.

### Default named template

```liquid
zone "{{ domain }}" {
	type master;
	file "{{ cfg_save_path }}";
};
```

### Default zone template

```liquid
;
; Zone file for {{ domain }}
;
; The full zone file
;
$TTL 3D
@       IN      SOA     ns.{{ domain }}. root.{{ domain }}. (
                        200608081       ; serial, todays date + todays serial # 
                        8H              ; refresh, seconds
                        2H              ; retry, seconds
                        4W              ; expire, seconds
                        1D )            ; minimum, seconds
;
                NS      ns              ; Inet Address of name server
;
ns              A       172.16.253.1
{% for record in dns_records %}
{{ record.subdomain }}   IN   {{ record.record_type }}    {{ record.value }}
{% endfor %}
```
